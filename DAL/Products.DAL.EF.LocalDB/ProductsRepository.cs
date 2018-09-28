namespace Products.DAL.EF.LocalDB
{
    using System.Data.SqlClient;

    using Products.Business.Entities;
    using Products.DAL.EF.Interfaces;

    public sealed class ProductsRepository : EfRepository<Product>
    {
        private EfContext context;

        public ProductsRepository(IEfRepositorySettings settings)
            : base(settings)
        {
        }

        protected override EfContext Context =>
            context ?? (context = new LocalDbContext(new SqlConnection(Settings.ConnectionString)));
    }
}
