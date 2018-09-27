namespace Products.Client.ProductsBrowser.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Prism.Mvvm;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.Infrastructure.Core.Interfaces;
    using Products.Infrastructure.DefaultLog;

    internal sealed class ProductsBrowserViewModel : BindableBase
    {
        private readonly IProductContract products;

        private readonly ILog log;

        private ObservableCollection<Product> productsFromDb;
        private ObservableCollection<Product> productsFromFile;

        public ProductsBrowserViewModel(IProductContract products, ILog log)
        {
            this.products = products ?? throw new ArgumentNullException(nameof(products));
            this.log = log ?? new DefaultLog();

            IsWaitingDownloadFromDb = true;
            RaisePropertyChanged(nameof(IsWaitingDownloadFromDb));
            Task.Run(
                async () => ProductsFromDb =
                                new ObservableCollection<Product>(await products.GetAllAsync().ConfigureAwait(false)))
                .ContinueWith(task =>
                    {
                        IsWaitingDownloadFromDb = false;
                        RaisePropertyChanged(nameof(IsWaitingDownloadFromDb));
                    });

            IsWaitingDownloadFromFile = true;
            RaisePropertyChanged(nameof(IsWaitingDownloadFromFile));
            Task.Run(() => ProductsFromFile = new ObservableCollection<Product>())
                .ContinueWith(task =>
                    {
                        IsWaitingDownloadFromFile = false;
                        RaisePropertyChanged(nameof(IsWaitingDownloadFromFile));
                    });
        }

        public bool IsWaitingDownloadFromDb { get; private set; }

        public ObservableCollection<Product> ProductsFromDb
        {
            get => productsFromDb;
            set => SetProperty(ref productsFromDb, value);
        }

        public bool IsWaitingDownloadFromFile { get; private set; }

        public ObservableCollection<Product> ProductsFromFile
        {
            get => productsFromFile;
            set => SetProperty(ref productsFromFile, value);
        }
    }
}
