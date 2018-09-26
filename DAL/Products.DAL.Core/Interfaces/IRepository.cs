namespace Products.DAL.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
        where T : class
    {
        Task AddAsync(params T[] entities);

        Task<IEnumerable<T>> GetAllAsync();

        Task SaveAsync();
    }
}
