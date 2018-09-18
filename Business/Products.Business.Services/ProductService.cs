namespace Products.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Products.Business.Entities;
    using Products.Business.Services.Contracts;

    public sealed class ProductService : IProductService
    {
        public ProductService()
        {
        }

        public Task AddAsync(Product product) => throw new NotImplementedException();

        public Task<IEnumerable<Product>> GetAllAsync() => throw new NotImplementedException();
    }
}
