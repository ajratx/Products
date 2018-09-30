namespace Products.Client.ProductsCreator.Validators
{
    using FluentValidation;

    using Products.Business.Entities;

    internal sealed class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле 'Наименование' должно быть заполнено")
                .Length(1, 100).WithMessage("Длина поля 'Наименование' должно быть в диапазоне от 1 до 100");   

            RuleFor(x => x.Price).GreaterThan(0M).WithMessage("Поле 'Цена' должна быть больше нуля"); 

            RuleFor(x => x.Count).GreaterThan(0).WithMessage("Поле 'Количество' должна быть больше нуля");
        }
    }
}
