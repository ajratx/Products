namespace Products.DAL.EF.SQLite
{
    using System.Data.Entity;

    using Products.DAL.EF.Contexts;

    public sealed class SqliteContext : Context
    {
        public SqliteContext(string connectionString)
            : base(connectionString)
        {
            DbConfiguration.SetConfiguration(new SqliteDbConfiguration());
        }
    }
}
