namespace Products.DAL.EF
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Produts.DAL;
    using Produts.DAL.Interfaces;

    public class Repository<T> : BaseRepository<T>
        where T : class
    {
        private readonly Context context;

        public Repository(IRepositorySettings settings)
            : base(settings) =>
            context = new Context(Settings.ConnectionString);

        public override async Task AddAsync(T entity)
        {
            await Task.Run(() => context.Set<T>().Add(entity)).ConfigureAwait(false);
        }

        public override Task<IEnumerable<T>> GetAllAsync() => throw new System.NotImplementedException();

        public override Task SaveAsync() => throw new System.NotImplementedException();
    }
}
