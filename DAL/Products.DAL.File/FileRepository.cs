namespace Products.DAL.File
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Products.DAL.Core.Interfaces;
    using Products.DAL.File.Interfaces;

    public sealed class FileRepository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private static ReaderWriterLockSlim fileLock = new ReaderWriterLockSlim();

        private readonly ConcurrentQueue<T> products;

        private readonly ISerializer<IEnumerable<T>> serializer;

        private bool disposed;

        public FileRepository(ISerializer<IEnumerable<T>> serializer)
        {
            products = new ConcurrentQueue<T>();
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task AddAsync(params T[] entities)
        {
            ThrowIfDisposed();
            await Task.Run(
                () =>
                    {
                        foreach (var entity in entities) products.Enqueue(entity);
                    }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            ThrowIfDisposed();
            fileLock.EnterReadLock();
            try
            {
                return await serializer.DeserializeAsync().ConfigureAwait(false);
            }
            finally
            {
                fileLock.ExitReadLock();
            }
        }

        public async Task SaveAsync()
        {
            ThrowIfDisposed();
            fileLock.EnterWriteLock();
            try
            {
                var allProducts = await GetAllAsync().ConfigureAwait(false);
                var notPushedProducts = products.Except(allProducts);

                await serializer.SerializeAsync(notPushedProducts).ConfigureAwait(false);
            }
            finally
            {
                fileLock.ExitWriteLock();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing) fileLock = null;

            disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (disposed) throw new ObjectDisposedException(GetType().FullName);
        }
    }
}
