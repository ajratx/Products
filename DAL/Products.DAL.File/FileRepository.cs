namespace Products.DAL.File
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Products.DAL.Core;
    using Products.DAL.File.Interfaces;

    public abstract class FileRepository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private readonly ConcurrentQueue<T> products;

        private bool disposed;

        protected FileRepository() => products = new ConcurrentQueue<T>();

        protected abstract ISerializer<T> Serializer { get; }

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

            if (Serializer == null) return null;

            return await Serializer.DeserializeAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync()
        {
            ThrowIfDisposed();

            if (Serializer == null) return;

            var allProducts = await GetAllAsync().ConfigureAwait(false);
            var notPushedProducts = products.Except(allProducts);

            await Serializer.SerializeAsync(notPushedProducts).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
                (Serializer as IDisposable)?.Dispose();

            disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (disposed) throw new ObjectDisposedException(GetType().FullName);
        }
    }
}
