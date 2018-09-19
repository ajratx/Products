namespace Products.DAL.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
        where T : class
    {
        Task AddAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task SaveAsync();
    }
}
