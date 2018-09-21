using System.Data.Entity;

namespace Products.DAL.EF.SQLite
{
    using System;
    using System.Linq;

    internal class SqliteDatabasebInitializer : IDatabaseInitializer<SqliteContext>
    {
        public void InitializeDatabase(SqliteContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var modelBuilder = new DbModelBuilder(DbModelBuilderVersion.Latest);
            var model = modelBuilder.Build(context.Database.Connection);
        }
    }
}
