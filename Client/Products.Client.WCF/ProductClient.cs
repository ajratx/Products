namespace Products.Client.WCF
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using Products.Business.Contracts;
    using Products.Business.Entities;

    public sealed class ProductClient : ClientBase<IProductContract>, IProductContract
    {
        public async Task AddAsync(params Product[] products)
        {
            await Channel.AddAsync(products).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await Channel.GetAllAsync().ConfigureAwait(false);
    }
}
