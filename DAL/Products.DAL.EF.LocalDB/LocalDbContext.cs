namespace Products.DAL.EF.LocalDB
{
    using System.Data.Common;
    using System.Data.Entity;

    [DbConfigurationType(typeof(LocalDbConfiguration))]
    public sealed class LocalDbContext : Context
    {
        public LocalDbContext(DbConnection connection)
            : base(connection)
        {
        }
    }
}
