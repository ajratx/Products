namespace Products.Core.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using Products.Core.Models;       

    [ServiceContract]
    public interface IProductsService
    {
        [OperationContract]
        Task AddAsync(Product product);

        [OperationContract]
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
