namespace Products.WebService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Products.Business.Contracts;
    using Products.Business.Entities;           
    using Products.DAL.Core.Interfaces;
    using Products.Infrastucture.Core;

    public sealed class ProductService : IProductContract
    {
        private readonly IRepository<Product> productRepository;
        private readonly ILog log;
        
        public ProductService(IRepository<Product> productRepository, ILog log)
        {
            this.productRepository = productRepository;
            this.log = log;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await productRepository.AddAsync(product).ConfigureAwait(false);
                await productRepository.SaveAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {   
                return await productRepository.GetAllAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
        }
    }
}
