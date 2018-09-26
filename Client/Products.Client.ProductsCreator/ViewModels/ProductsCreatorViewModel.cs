namespace Products.Client.ProductsCreator.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Nito.Mvvm;

    using Prism.Mvvm;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.Infrastructure.Core.Interfaces;
    using Products.Infrastructure.DefaultLog;

    internal sealed class ProductsCreatorViewModel : BindableBase
    {
        private readonly IProductContract products;
        private readonly ILog log;
        private string name;
        private decimal price;
        private int count;

        public ProductsCreatorViewModel(IProductContract products, ILog log)
        {
            this.products = products ?? throw new ArgumentNullException(nameof(products));
            this.log = log ?? new DefaultLog();

            CreateProductCommand = new AsyncCommand(CreateProductAsync);
        }

        public IAsyncCommand CreateProductCommand { get; set; }

        public bool CreatingIsSuccessfullyCompleted { get; private set; }

        public bool CreatingIsFaulted { get; private set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        private async Task CreateProductAsync()
        {
            HideInfoAboutCreating();

            try
            {
                var newProduct = new Product { Name = Name, Price = Price, Count = Count };

                await products.AddAsync(new [] { newProduct }).ConfigureAwait(false);

                SayCreatingSuccessfullyCompleted();
            }
            catch (Exception e)
            {
                log.Error(e);
                SayCreaingFaulted();
            }
        }

        private void HideInfoAboutCreating()
        {
            CreatingIsSuccessfullyCompleted = false;
            CreatingIsFaulted = false;

            RaisePropertyChanged(nameof(CreatingIsSuccessfullyCompleted));
            RaisePropertyChanged(nameof(CreatingIsFaulted));
        }

        private void SayCreatingSuccessfullyCompleted()
        {
            CreatingIsSuccessfullyCompleted = true;
            RaisePropertyChanged(nameof(CreatingIsSuccessfullyCompleted));
        }

        private void SayCreaingFaulted()
        {
            CreatingIsFaulted = true;
            RaisePropertyChanged(nameof(CreatingIsFaulted));
        }
    }
}
