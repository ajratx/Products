namespace Products.DAL.EF.Contexts
{
    using System.Data.Entity;

    using Products.Business.Entities;
    using Products.DAL.EF.Configurations;

    public abstract class Context : DbContext
    {
        protected Context(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
