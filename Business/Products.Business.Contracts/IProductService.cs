namespace Products.Business.Contracts
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using Products.Business.Entities;

    [ServiceContract]
    public interface IProductContract
    {
        [OperationContract]
        Task AddAsync(Product product);

        [OperationContract]
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
