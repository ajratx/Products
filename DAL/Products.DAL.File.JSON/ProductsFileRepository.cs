namespace Products.DAL.File.JSON
{
    using Products.Business.Entities;
    using Products.DAL.File.Interfaces;

    public sealed class ProductsFileRepository : FileRepository<Product>
    {
        public ProductsFileRepository(ISerializerSettings settings)
            => Serializer = new JsonSerializer<Product>(settings);

        protected override ISerializer<Product> Serializer { get; }
    }
}
