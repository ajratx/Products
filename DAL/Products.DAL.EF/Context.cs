namespace Products.DAL.EF
{
    using System.Data.Common;
    using System.Data.Entity;

    using Products.Business.Entities;
    using Products.DAL.EF.Configurations;

    public abstract class Context : DbContext
    {
        protected Context(DbConnection connection)
            : base(connection, true)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
