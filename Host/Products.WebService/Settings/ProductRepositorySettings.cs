namespace Products.WebService.Settings
{
    using Products.DAL.EF.Interfaces;

    public class ProductRepositorySettings : IEfRepositorySettings
    {
        public string ConnectionString { get; set; }
    }
}