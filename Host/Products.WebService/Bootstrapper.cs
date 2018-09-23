namespace Products.WebService
{
    using System.Configuration;

    using Autofac;
    using Products.Business.Contracts;
    using Products.Business.Entities;        
    using Products.DAL.Core.Interfaces;
    using Products.DAL.EF.Interfaces;
    using Products.DAL.EF.LocalDB;   
    using Products.Infrastructure.DefaultLogger;
    using Products.Infrastucture.Core;
    using Products.WebService.Models;
    using Products.WebService.Services;

    public static class Bootstrapper
    {
        private const string ConnectioStringName = "Products";

        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            var repositorySetting = new ProductRepositorySettings
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[ConnectioStringName].ConnectionString
            };

            builder.RegisterInstance<IRepositorySettings>(repositorySetting);
            builder.RegisterType<ProductRepository>().As<IRepository<Product>>();
            builder.RegisterType<ProductService>().As<IProductContract>();
            builder.RegisterType<DefaultLog>().As<ILog>();

            return builder.Build();
        }
    }
}