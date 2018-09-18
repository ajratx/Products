namespace Products.Business.Services.Contracts
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using Products.Business.Entities;

    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        Task AddAsync(Product product);

        [OperationContract]
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
