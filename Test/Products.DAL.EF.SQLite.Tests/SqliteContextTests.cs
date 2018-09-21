namespace Products.DAL.EF.SQLite.Tests
{
    using System.Data.SQLite;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Products.Business.Entities;

    [TestClass]
    public class SqliteContextTests
    {
        [TestMethod]
        public void Constructor()
        {
            var connection = new SQLiteConnection("Data Source=Products.sqlite;Version=3;");
            var context = new SqliteContext(connection);

            context.Set<Product>().Add(new Product { Name = "Book", Price = 25.6m, Count = 8 });
            context.SaveChanges();
        }
    }
}