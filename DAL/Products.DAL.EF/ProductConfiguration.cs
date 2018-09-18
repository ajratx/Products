namespace Products.DAL.EF
{
    using System.Data.Entity.ModelConfiguration;

    using Products.Business.Entities;

    internal sealed class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(product => product.Id);
            Property(product => product.Name).IsRequired().HasMaxLength(100);
        }
    }
}
