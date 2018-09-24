namespace Products.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.DAL.Core.Interfaces;
    using Products.Infrastucture.Core;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public sealed class ProductService : IProductContract, IDisposable
    {
        private readonly IRepository<Product> productRepository;
        private readonly ILog log;

        private bool disposed;

        public ProductService(IRepository<Product> productRepository, ILog log)
        {
            this.productRepository = productRepository;
            this.log = log;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                ThrowIfDisposed();
                await productRepository.AddAsync(product).ConfigureAwait(false);
                await productRepository.SaveAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                ThrowIfDisposed();
                return await productRepository.GetAllAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void ThrowIfDisposed()
        {
            if (disposed) throw new ObjectDisposedException(GetType().FullName);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
                (productRepository as IDisposable)?.Dispose();

            disposed = true;
        }
    }
}
