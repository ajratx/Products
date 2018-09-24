namespace Products.DAL.EF.SQLite.Tests
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Products.Business.Entities;
    using Products.DAL.EF.LocalDB;

    [TestClass]
    public class LocalDbContextTests
    {
        private LocalDbContext context, anotherContext;
        private SqlConnection sqlConnection;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullConnection_ExceptionThrown()
        {
            context = new LocalDbContext(sqlConnection);
        }

        [TestMethod]
        public void AddTwoEntities_GetAll()
        {
            sqlConnection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=ProductsTest");
            context = new LocalDbContext(sqlConnection);
            anotherContext = new LocalDbContext(sqlConnection);

            if (!context.Database.Exists()) context.Database.Create();

            var products = context.Set<Product>();

            products.Add(new Product { Name = "Product #1", Price = 1.23m, Count = 8 });
            products.Add(new Product { Name = "Product #2", Price = 4.56m, Count = 3 });
            context.SaveChanges();

            var allProducts = anotherContext.Set<Product>().ToArray();

            Assert.IsNotNull(allProducts);
            Assert.AreEqual(2, allProducts.Length);
            Assert.AreEqual("Product #1", allProducts[0].Name);
            Assert.AreEqual(1.23m, allProducts[0].Price);
            Assert.AreEqual(8, allProducts[0].Count);
            Assert.AreEqual("Product #2", allProducts[1].Name);
            Assert.AreEqual(4.56m, allProducts[1].Price);
            Assert.AreEqual(3, allProducts[1].Count);
        }

        [TestCleanup]
        public void CleanUp()
        {
            // context?.Database?.Delete();

            context?.Dispose();
            anotherContext?.Dispose();
        }
    }
}