namespace Products.DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Products.DAL.EF.Contexts;
    using Products.DAL.EF.Interfaces;
    using Products.DAL.Interfaces;

    public sealed class Repository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private readonly Context context;

        private bool disposed;

        public Repository(IRepositorySettings settings)
        {
            CheckSettings(settings);
            context = new Context(settings.ConnectionString);
            CheckContext(context);
        }

        public async Task AddAsync(T entity)
        {
            ThrowIfDisposed();
            await Task.Run(() => context.Set<T>().Add(entity)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            ThrowIfDisposed();
            return await context.Set<T>().ToArrayAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync()
        {
            ThrowIfDisposed();
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private static void CheckSettings(IRepositorySettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new ArgumentException(
                    "ConnectionString can't be null or white space",
                    nameof(settings.ConnectionString));
        }

        private static void CheckContext(DbContext context)
        {
            if (context.Set<T>() == null)
                throw new ArgumentException("Context has no contains requarement set", nameof(context));
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing) context.Dispose();

            disposed = true;
        }
    }
}
