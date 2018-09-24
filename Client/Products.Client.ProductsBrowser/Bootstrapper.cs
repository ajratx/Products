namespace Products.Client.ProductsBrowser
{      
    using Autofac;
    using Products.Business.Contracts;
    using Products.Client.ProductsBrowser.Views;
    using Products.Client.WCF;
    using Products.Client.WPF.Common.Utility;
    using Products.Infrastructure.DefaultLogger;
    using Products.Infrastucture.Core;

    internal sealed class Bootstrapper : AppBootstrapper<ProductsBrowserView>
    {
        protected override void ConfigureBuilder(ContainerBuilder builder)
        {
            builder.Register(x => new DefaultLog()).As<ILog>().SingleInstance();
            builder.Register(x => new ProductClient()).As<IProductContract>();
        }
    }
}
