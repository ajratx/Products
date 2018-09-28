namespace Products.WebService
{
    using System.Configuration;

    using Autofac;
    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.Business.Services;
    using Products.DAL.Core;
    using Products.DAL.EF.Interfaces;
    using Products.DAL.EF.LocalDB;
    using Products.Infrastructure.Core.Interfaces;
    using Products.Infrastructure.DefaultLog;
    using Products.WebService.Models;

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

            builder.RegisterInstance<IEfRepositorySettings>(repositorySetting).SingleInstance();
            builder.Register(x => new DefaultLog()).As<ILog>().SingleInstance();
            builder.RegisterType<ProductsRepository>().As<IRepository<Product>>();
            builder.RegisterType<ProductService>().As<IProductContract>();

            return builder.Build();
        }
    }
}