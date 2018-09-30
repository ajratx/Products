namespace Products.Client.ProductsBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using Autofac;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.Client.ProductsBrowser.Views;
    using Products.Client.WCF;
    using Products.Client.WPF.Core.Application;
    using Products.Client.WPF.Core.Settings;
    using Products.DAL.File;
    using Products.DAL.File.Interfaces;
    using Products.DAL.File.JSON;
    using Products.Infrastructure.Core;
    using Products.Infrastructure.DefaultLog;


    internal sealed class Bootstrapper : AppBootstrapper<ProductsBrowserView>
    {      
        private const string StorageFilePathParameter = "StorageFilePath";

        protected override void ConfigureBuilder(ContainerBuilder builder)
        {
            var storageFilePath = ConfigurationManager.AppSettings[StorageFilePathParameter];
            if (string.IsNullOrWhiteSpace(storageFilePath))
                throw new ApplicationException("В файле конфигурации отсутствует параметр для подключения к файлу хранения продуктов");

            var serializerSettings = new SerializerSettings
            {
                FilePath = storageFilePath
            };

            builder.Register(x => new DefaultLog()).As<ILog>().SingleInstance();
            builder.Register(x => new ProductClient()).As<IProductContract>();
            builder.RegisterInstance<ISerializerSettings>(serializerSettings);
            builder.Register(x => new ProductComparer()).As<IEqualityComparer<Product>>();
            builder.RegisterType<ProductsFileRepository>().As<FileRepository<Product>>();
        }
    }
}
