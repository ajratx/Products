namespace ProductsService
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Products.Core.Interfaces;
    using Products.Core.Models;

    public class ProductsService : IProductsService
    {
        public Task AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
