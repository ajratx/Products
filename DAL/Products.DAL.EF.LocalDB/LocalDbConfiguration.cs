namespace Products.DAL.EF.LocalDB
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.SqlServer;

    internal sealed class LocalDbConfiguration : DbConfiguration
    {
        internal LocalDbConfiguration()
        {
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}
