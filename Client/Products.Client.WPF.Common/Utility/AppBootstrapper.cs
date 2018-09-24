namespace Products.Client.WPF.Common.Utility
{
    using System.Windows;

    using Autofac;

    using Prism.Autofac;

    public abstract class AppBootstrapper<T> : AutofacBootstrapper
        where T : Window
    {
        protected override DependencyObject CreateShell() => Container.Resolve<T>();

        protected override void InitializeShell()
        {
            Application.Current?.MainWindow?.Show();
        }

        protected override ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();

            ConfigureBuilder(builder);

            return builder;
        }

        protected abstract void ConfigureBuilder(ContainerBuilder builder);
    }
}
