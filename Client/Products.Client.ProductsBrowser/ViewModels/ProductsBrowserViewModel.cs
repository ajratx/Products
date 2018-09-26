namespace Products.Client.ProductsBrowser.ViewModels
{
    using System;

    using Products.Business.Contracts;
    using Products.Infrastructure.Core.Interfaces;
    using Products.Infrastructure.DefaultLog;

    internal sealed class ProductsBrowserViewModel
    {
        private readonly IProductContract products;
        private readonly ILog log;

        public ProductsBrowserViewModel(IProductContract products, ILog log)
        {
            this.products = products ?? throw new ArgumentNullException(nameof(products));
            this.log = log ?? new DefaultLog();
        }
    }
}
