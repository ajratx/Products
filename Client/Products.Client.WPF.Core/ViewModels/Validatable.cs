namespace Products.Client.WPF.Core.ViewModels
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using FluentValidation;

    using Prism.Mvvm;

    public abstract class ValidatableBase<T>
        : BindableBase, INotifyDataErrorInfo
    {
        private readonly IValidator<T> validator;

        protected ValidatableBase(IValidator<T> validator)
            => this.validator = validator ?? throw new ArgumentNullException(nameof(validator));

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => !validator.Validate(Model).IsValid;

        protected abstract T Model { get; }

        public IEnumerable GetErrors(string propertyName)
        {
            var errors = validator.Validate(Model).Errors
                .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
                .ToDictionary(x => x.Key, x => x.ToList());

            if (string.IsNullOrEmpty(propertyName))
                return errors.SelectMany(x => x.Value.ToList());

            if (errors.ContainsKey(propertyName) && errors[propertyName] != null)
                return errors[propertyName];
            return new string[] { };
        }

        protected void RaiseOnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
