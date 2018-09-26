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
        Task AddAsync(params Product[] products);

        [OperationContract]
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
