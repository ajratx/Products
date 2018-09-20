namespace ProductsCreator.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class MainViewModel : INotifyPropertyChanged
    {
        private string name;

        private decimal price;

        private int count;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateCommand { get; set; }

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
                await Task.FromResult<object>(null).ConfigureAwait(false);
                /*
                var newProduct = new Product();
                newProduct.Name = Name;
                newProduct.Price = Price;
                newProduct.Count = Count;

                await productsService.AddAsync(newProduct);
                */
            }
            catch (Exception e)
            {
                await Task.FromException(e).ConfigureAwait(false);
            }
        }
    }
}
