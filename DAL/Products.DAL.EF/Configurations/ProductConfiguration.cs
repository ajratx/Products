namespace Products.DAL.EF.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Products.Business.Entities;

    public sealed class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(product => product.Id);
            Property(product => product.Name).IsRequired().HasMaxLength(100);
        }
    }
}
