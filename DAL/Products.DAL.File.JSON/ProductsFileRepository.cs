namespace Products.DAL.File.JSON
{
    using System.Collections.Generic;

    using Products.Business.Entities;
    using Products.DAL.File.Interfaces;

    public sealed class ProductsFileRepository : FileRepository<Product>
    {
        public ProductsFileRepository(
            ISerializerSettings settings, 
            IEqualityComparer<Product> comparer = null)
            : base(comparer)
            => Serializer = new JsonSerializer<Product>(settings);

        protected override ISerializer<Product> Serializer { get; }
    }
}
