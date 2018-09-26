namespace Products.DAL.EF.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Products.Business.Entities;

    internal sealed class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(product => product.Id);
            HasIndex(product => product.Name).IsUnique();
            Property(product => product.Name).IsRequired().HasMaxLength(100);
        }
    }
}
