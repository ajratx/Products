namespace Products.DAL.EF
{
    using System.Data.Entity;

    using Products.Business.Entities;

    public sealed class Context : DbContext
    {
        public Context(string connectionString)
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
