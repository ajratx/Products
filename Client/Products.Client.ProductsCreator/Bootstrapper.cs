namespace Products.Client.ProductsCreator
{
    using Autofac;

    using Products.Business.Contracts;
    using Products.Client.ProductsCreator.Views;
    using Products.Client.WCF;
    using Products.Client.WPF.Common.Utility;
    using Products.Infrastructure.DefaultLogger;
    using Products.Infrastucture.Core;

    internal sealed class Bootstrapper : AppBootstrapper<ProductsCreatorView>
    {
        protected override void ConfigureBuilder(ContainerBuilder builder)
        {
            builder.Register(x => new DefaultLog()).As<ILog>().SingleInstance();
            builder.Register(x => new ProductClient()).As<IProductContract>();
        }
    }
}
