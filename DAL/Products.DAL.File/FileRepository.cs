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
        private readonly ConcurrentStack<T> entitiesStack;

        private readonly IEqualityComparer<T> comparer;

        private bool disposed;

        protected FileRepository(IEqualityComparer<T> comparer = null)
        {
            entitiesStack = new ConcurrentStack<T>();
            this.comparer = comparer;
        } 

        protected abstract ISerializer<T> Serializer { get; }

        public async Task AddAsync(params T[] entities)
        {
            ThrowIfDisposed();
            await Task.Run(
                () =>
                    { 
                        foreach (var entity in entities)
                            entitiesStack.Push(entity);
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

            var entitiesInFile = await GetAllAsync().ConfigureAwait(false) ?? new T[] { };
            var entitiesToSave = entitiesStack.Union(entitiesInFile, comparer);

            await Serializer.SerializeAsync(entitiesToSave).ConfigureAwait(false);

            entitiesStack.Clear();
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
