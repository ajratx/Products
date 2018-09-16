namespace ProductsCreator.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Products.Core.Interfaces;
    using Products.Core.Models;

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IProductsService productsService;

        private string name;

        private decimal price;

        private int count;

        public MainViewModel(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public ICommand CreateCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prorertyName = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prorertyName));

        private async Task CreateProductAsync()
        {
            try
            {
                var newProduct = new Product();
                newProduct.Name = Name;
                newProduct.Price = Price;
                newProduct.Count = Count;

                await productsService.AddAsync(newProduct);
            }
            catch (Exception e)
            {
            }
        }
    }
}
