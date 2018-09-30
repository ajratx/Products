namespace Products.DAL.File.JSON
{
    using System.Collections.Generic;

    using Products.Business.Entities;

    public sealed class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (ReferenceEquals(x, y)) return true;

            return ReferenceEquals(x, null) || ReferenceEquals(y, null) 
                ? false 
                : x.Name == y.Name;
        }

        public int GetHashCode(Product product) => product?.Name?.GetHashCode() ?? 0; 
    }
}
