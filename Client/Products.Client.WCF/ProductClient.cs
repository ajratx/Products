namespace Products.Client.WCF
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using Products.Business.Contracts;
    using Products.Business.Entities;

    public sealed class ProductClient : ClientBase<IProductContract>, IProductContract
    {
        public async Task AddAsync(Product product)
        {
            await Channel.AddAsync(product).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await Channel.GetAllAsync().ConfigureAwait(false);
    }
}
