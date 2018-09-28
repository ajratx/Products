namespace Products.DAL.File.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISerializer<T>
    {
        Task SerializeAsync(IEnumerable<T> items);

        Task<IEnumerable<T>> DeserializeAsync();
    }
}