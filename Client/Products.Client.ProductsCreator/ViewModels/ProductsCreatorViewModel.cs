namespace Products.Client.ProductsCreator.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Prism.Mvvm;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.Infrastructure.DefaultLogger;
    using Products.Infrastucture.Core;

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
        }

        public ICommand CreateProductCommand { get; set; }

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
            try
            {
                var newProduct = new Product
                {
                    Name = Name,
                    Price = Price,
                    Count = Count
                };

                await products.AddAsync(newProduct);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }
    }
}
