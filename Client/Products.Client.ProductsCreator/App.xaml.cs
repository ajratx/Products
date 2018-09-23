namespace Products.Client.ProductsCreator
{
    using System.Windows;

    using Products.Client.ProductsBrowser;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new Bootstrapper().Run();
        }
    }
}
