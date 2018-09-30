namespace Products.Client.ProductsCreator
{                 
    using System.Windows;

    using Products.Client.ProductsCreator.Views;
    using Products.Client.WPF.Core.Application;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DispatcherUnhandledException += Application_DispatcherUnhandledException;        
            Bootstapper.Run();
        }

        protected AppBootstrapper<ProductsCreatorView> Bootstapper => new Bootstrapper();

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"Непредвиденная ошибка: {e.Exception.Message}",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            e.Handled = true;
        }
    }
}
