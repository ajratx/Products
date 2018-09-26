namespace Products.Client.ProductsCreator.ValidationRules
{
    using System.Globalization;
    using System.Windows.Controls;

    internal class NotEmptyString : ValidationRule
    {
        private const string ErrorMessage = "Поле не заполнено";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringValue = value as string;

            return !string.IsNullOrWhiteSpace(stringValue)
                       ? ValidationResult.ValidResult
                       : new ValidationResult(false, ErrorMessage);
        }
    }
}
