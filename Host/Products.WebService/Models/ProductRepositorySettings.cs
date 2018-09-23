namespace Products.WebService.Models
{
    using Products.DAL.EF.Interfaces;

    public class ProductRepositorySettings : IRepositorySettings
    {
        public string ConnectionString { get; set; }
    }
}