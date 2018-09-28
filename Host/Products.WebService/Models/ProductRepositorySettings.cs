namespace Products.WebService.Models
{
    using Products.DAL.EF.Interfaces;

    public class ProductRepositorySettings : IEfRepositorySettings
    {
        public string ConnectionString { get; set; }
    }
}