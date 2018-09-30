namespace Products.Client.ProductsBrowser.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Nito.Mvvm;
    using Prism.Mvvm;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.DAL.File;
    using Products.DAL.File.JSON;
    using Products.Infrastructure.Core;
    using Products.Infrastructure.DefaultLog;

    internal sealed class ProductsBrowserViewModel : BindableBase
    {
        private readonly IProductContract productsServiceClient;
        private readonly FileRepository<Product> fileRepository;
        private readonly IEqualityComparer<Product> productComparer;
        private readonly ILog log;

        private ObservableCollection<Product> productsFromDb;
        private ObservableCollection<Product> notUploadedToDbProducts;

        public ProductsBrowserViewModel(
            IProductContract productsServiceClient, 
            FileRepository<Product> fileRepository, 
            IEqualityComparer<Product> productComparer,
            ILog log)
        {
            this.productsServiceClient = productsServiceClient 
                ?? throw new ArgumentNullException(nameof(productsServiceClient));
            this.fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            this.productComparer = productComparer ?? new ProductComparer();
            this.log = log ?? new DefaultLog();

            UploadToDatabase = new AsyncCommand(UploadToDatabaseAsync);
    
            Task.Run(async () => await SetProductsFromDbAsync().ConfigureAwait(true));
            Task.Run(async () => await SetNotUploadedToDbProductsAsync().ConfigureAwait(true));
        }

        public IAsyncCommand UploadToDatabase { get; set; }

        public bool UploadIsSuccessfullyCompleted { get; private set; }

        public bool UploadIsFaulted { get; private set; }

        public bool IsWaitingDownloadFromDb { get; private set; }

        public ObservableCollection<Product> ProductsFromDb
        {
            get => productsFromDb;
            set => SetProperty(ref productsFromDb, value);
        }

        public bool IsWaitingDownloadFromFile { get; private set; }

        public ObservableCollection<Product> NotUploadedToDbProducts
        {
            get => notUploadedToDbProducts;
            set => SetProperty(ref notUploadedToDbProducts, value);
        }

        private async Task UploadToDatabaseAsync()
        {
            HideUploadingInfo();

            try
            {
                await productsServiceClient.AddAsync(NotUploadedToDbProducts.ToArray()).ConfigureAwait(false);
                SayUploadIsSuccessfully();
            }
            catch (Exception e)
            {
                log.Error(e);
                SayUploadIsFaulted();
            }

            await SetNotUploadedToDbProductsAsync().ConfigureAwait(false);
            await SetProductsFromDbAsync().ConfigureAwait(false);
        }

        private async Task SetProductsFromDbAsync()
        {
            IsWaitingDownloadFromDb = true;
            RaisePropertyChanged(nameof(IsWaitingDownloadFromDb));    

            try
            {
                var products = await productsServiceClient.GetAllAsync().ConfigureAwait(false);
                ProductsFromDb = new ObservableCollection<Product>(products);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            IsWaitingDownloadFromDb = false;
            RaisePropertyChanged(nameof(IsWaitingDownloadFromDb));
        }

        private async Task SetNotUploadedToDbProductsAsync()
        {
            IsWaitingDownloadFromFile = true;
            RaisePropertyChanged(nameof(IsWaitingDownloadFromFile));

            try
            {
                var productInDb = await productsServiceClient.GetAllAsync().ConfigureAwait(false);
                var productsInFile = await fileRepository.GetAllAsync().ConfigureAwait(false);

                var notUploadedProducts = productsInFile.Except(productInDb, productComparer);
                NotUploadedToDbProducts = new ObservableCollection<Product>(notUploadedProducts);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            IsWaitingDownloadFromFile = false;
            RaisePropertyChanged(nameof(IsWaitingDownloadFromFile));
        }

        private void HideUploadingInfo()
        {
            UploadIsSuccessfullyCompleted = false;
            UploadIsFaulted = false;

            RaisePropertyChanged(nameof(UploadIsSuccessfullyCompleted));
            RaisePropertyChanged(nameof(UploadIsFaulted));
        }

        private void SayUploadIsSuccessfully()
        {
            UploadIsSuccessfullyCompleted = true;       
            RaisePropertyChanged(nameof(UploadIsSuccessfullyCompleted)); 
        }

        private void SayUploadIsFaulted()
        {
            UploadIsFaulted = true;
            RaisePropertyChanged(nameof(UploadIsFaulted));
        }
    }
}
