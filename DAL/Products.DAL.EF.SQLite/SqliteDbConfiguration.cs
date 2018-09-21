namespace Products.DAL.EF.SQLite
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Common;
    using System.Data.SQLite;
    using System.Data.SQLite.EF6;

    internal sealed class SqliteDbConfiguration : DbConfiguration
    {
        internal SqliteDbConfiguration()
        {
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);

            SetProviderServices(
                "System.Data.SQLite",
                (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}
