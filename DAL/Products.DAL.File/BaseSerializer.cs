namespace Products.DAL.File
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Products.DAL.File.Interfaces;

    public abstract class BaseSerializer<T> : ISerializer<T>, IDisposable
    {
        private static readonly SemaphoreSlim FileLock = new SemaphoreSlim(1);

        private bool disposed;

        protected BaseSerializer(ISerializerSettings settings)
        {
            CheckSettings(settings);
            Settings = settings;
        }

        protected ISerializerSettings Settings { get; }

        public async Task SerializeAsync(IEnumerable<T> items)
        {
            ThrowIfDisposed();
            try
            {
                await FileLock.WaitAsync().ConfigureAwait(false);
                try
                {
                    await InternalSerializeAsync(items).ConfigureAwait(true);
                }
                finally
                {
                    FileLock.Release();
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        public async Task<IEnumerable<T>> DeserializeAsync()
        {
            ThrowIfDisposed();
            try
            {
                await FileLock.WaitAsync().ConfigureAwait(false);
                try
                {
                    return await InternalDeserializeAsync().ConfigureAwait(true);
                }
                finally
                {
                    FileLock.Release();
                }
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected abstract Task InternalSerializeAsync(IEnumerable<T> items);

        protected abstract Task<IEnumerable<T>> InternalDeserializeAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                FileLock.Release();
                FileLock.Dispose();
            }

            disposed = true;
        }

        private static void CheckSettings(ISerializerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(settings.FilePath))
                throw new ArgumentException("File path can't be null, empty or white space");
        }

        private void ThrowIfDisposed()
        {
            if (disposed) throw new ObjectDisposedException(GetType().FullName);
        }
    }
}
