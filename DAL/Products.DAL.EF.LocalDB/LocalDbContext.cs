namespace Products.DAL.EF.LocalDB
{
    using System.Data.Common;
    using System.Data.Entity;

    [DbConfigurationType(typeof(LocalDbConfiguration))]
    public sealed class LocalDbContext : EfContext
    {
        public LocalDbContext(DbConnection connection)
            : base(connection)
        {
        }
    }
}
