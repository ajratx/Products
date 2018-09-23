using Products.Infrastucture.Core;

namespace Products.Client.ProductsBrowser.ViewModels
{
    internal sealed class ProductsBrowserViewModel
    {
        private readonly ILog log;

        public ProductsBrowserViewModel(ILog log)
        {
            this.log = log;
        }
    }
}
