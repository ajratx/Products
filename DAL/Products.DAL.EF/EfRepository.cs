namespace Products.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Products.DAL.Core;
    using Products.DAL.EF.Interfaces;

    public abstract class EfRepository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private bool disposed;

        protected EfRepository(IEfRepositorySettings settings)
        {
            CheckSettings(settings);
            Settings = settings;
        }

        protected IEfRepositorySettings Settings { get; }

        protected abstract EfContext Context { get; }

        public async Task AddAsync(params T[] entities)
        {
            ThrowIfDisposed();
            await Task.Run(() => Context.Set<T>()?.AddRange(entities)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            ThrowIfDisposed();
            return await Context.Set<T>().ToArrayAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync()
        {
            ThrowIfDisposed();
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private static void CheckSettings(IEfRepositorySettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new ArgumentException(
                    "ConnectionString can't be null or white space",
                    nameof(settings.ConnectionString));
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing) Context.Dispose();

            disposed = true;
        }
    }
}
