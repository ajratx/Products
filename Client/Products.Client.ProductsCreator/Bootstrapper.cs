namespace Products.Client.ProductsBrowser
{
    using System.Windows;

    using Autofac;

    using Prism.Autofac;

    using Products.Client.WCF;
    using Products.Business.Contracts;
    using Products.Client.ProductsCreator.Views;
    using Products.Infrastructure.DefaultLogger;
    using Products.Infrastucture.Core;

    internal sealed class Bootstrapper : AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ProductsCreatorView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DefaultLog>().As<ILog>();
            builder.RegisterType<ProductClient>().As<IProductContract>();

            return builder;
        }
    }
}
