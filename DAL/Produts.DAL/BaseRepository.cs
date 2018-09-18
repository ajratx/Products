namespace Produts.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Produts.DAL.Interfaces;

    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        protected BaseRepository(IRepositorySettings settings)
        {
            CheckSettings(settings);
            Settings = settings;
        }

        protected IRepositorySettings Settings { get; }

        public abstract Task AddAsync(T entity);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task SaveAsync();

        private static void CheckSettings(IRepositorySettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new ArgumentException(
                    "Connection string can't be null or white space",
                    nameof(settings.ConnectionString));
        }
    }
}
