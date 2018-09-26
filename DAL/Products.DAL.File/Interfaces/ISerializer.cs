namespace Products.DAL.File.Interfaces
{
    using System.Threading.Tasks;

    public interface ISerializer<T>
    {
        Task SerializeAsync(T obj);

        Task<T> DeserializeAsync();
    }
}