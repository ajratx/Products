namespace Products.DAL.File
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Products.DAL.File.Interfaces;

    public abstract class BaseSerializer<T> : ISerializer<T>
    {

        protected BaseSerializer(ISerializerSettings settings)
        {
            CheckSettings(settings);
            Settings = settings;
        }

        protected ISerializerSettings Settings { get; }

        public abstract Task SerializeAsync(IEnumerable<T> items);

        public abstract Task<IEnumerable<T>> DeserializeAsync();

        private static void CheckSettings(ISerializerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(settings.FilePath))
                throw new ArgumentException("File path can't be null, empty or white space");
        }
    }
}
