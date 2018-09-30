namespace Products.Client.ProductsCreator.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using FluentValidation;
    using Nito.Mvvm;

    using Products.Business.Contracts;
    using Products.Business.Entities;
    using Products.Client.WPF.Core.ViewModels;
    using Products.DAL.File;
    using Products.Infrastructure.Core;
    using Products.Infrastructure.DefaultLog;

    internal sealed class ProductsCreatorViewModel : ValidatableBase<Product>
    {
        private readonly IProductContract productsServiceClient;
        private readonly FileRepository<Product> fileRepository;
        private readonly ILog log;
        private string name;
        private decimal price;
        private int count;

        private bool createdInDb;
        private bool createdInFile;

        public ProductsCreatorViewModel(
            IProductContract productsServiceClient,
            FileRepository<Product> fileRepository,
            AbstractValidator<Product> validator, 
            ILog log)
            : base(validator)
        {
            this.productsServiceClient = productsServiceClient 
                ?? throw new ArgumentNullException(nameof(productsServiceClient));

            this.fileRepository = fileRepository
                ?? throw new ArgumentNullException(nameof(fileRepository));

            this.log = log ?? new DefaultLog();

            CreateProductCommand = new AsyncCommand(CreateProductAsync);
        }

        public IAsyncCommand CreateProductCommand { get; set; }

        public bool CreatingIsSuccessfullyCompleted { get; private set; }

        public bool CreatingIsSuccessPartially { get; private set; }

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

        protected override Product Model => new Product { Name = Name, Price = Price, Count = Count };

        private async Task CreateProductAsync()
        {
            HideInfoAboutCreating();

            await AddProductToDatabaseAsync();
            await AddProductToFileStorageAsync();

            ShowCreatingInfo();
        }

        private async Task AddProductToDatabaseAsync()
        {   
            try
            {     
                await productsServiceClient.AddAsync(Model).ConfigureAwait(false);
                createdInDb = true;
            }
            catch (Exception e)
            {
                log.Error(e);
                createdInDb = false;
            }
        }

        private async Task AddProductToFileStorageAsync()
        {
            try
            {
                await fileRepository.AddAsync(Model).ConfigureAwait(false);
                await fileRepository.SaveAsync();
                createdInFile = true;
            }
            catch (Exception e)
            {
                log.Error(e);
                createdInFile = false;
            }
        }

        private void HideInfoAboutCreating()
        {
            CreatingIsSuccessfullyCompleted = false;
            CreatingIsSuccessPartially = false;
            CreatingIsFaulted = false;

            RaisePropertyChanged(nameof(CreatingIsSuccessfullyCompleted));
            RaisePropertyChanged(nameof(CreatingIsSuccessPartially));
            RaisePropertyChanged(nameof(CreatingIsFaulted));
        }

        private void ShowCreatingInfo()
        {
            if (createdInDb && createdInFile)
                SayCreatingSuccessfullyCompleted();
            else
            {
                if (!createdInDb && !createdInFile) SayCreaingFaulted(); 
                else SayCreatingSuccessPartially();
            }
        }

        private void SayCreatingSuccessfullyCompleted()
        {
            CreatingIsSuccessfullyCompleted = true;
            RaisePropertyChanged(nameof(CreatingIsSuccessfullyCompleted));
        }

        private void SayCreatingSuccessPartially()
        {
            CreatingIsSuccessPartially = true;
            RaisePropertyChanged(nameof(CreatingIsSuccessPartially));
        }

        private void SayCreaingFaulted()
        {
            CreatingIsFaulted = true;
            RaisePropertyChanged(nameof(CreatingIsFaulted));
        }
    }
}
