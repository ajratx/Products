namespace Products.DAL.EF.SQLite
{
    using System.Data.Common;
    using System.Data.Entity;

    public sealed class SqliteContext : Context
    {
        public SqliteContext(DbConnection connection)
            : base(connection)
        {
            DbConfiguration.SetConfiguration(new SqliteDbConfiguration());
        }
    }
}
