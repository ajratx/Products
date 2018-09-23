using Products.Business.Entities;
using Products.DAL.EF.Interfaces;
using System.Data.SqlClient;

namespace Products.DAL.EF.LocalDB
{
    public sealed class ProductRepository : Repository<Product>
    {
        private Context context;

        public ProductRepository(IRepositorySettings settings) 
            : base(settings)
        {   
        }

        protected override Context Context =>
            context ?? (context = new LocalDbContext(new SqlConnection(Settings.ConnectionString)));
    }
}
