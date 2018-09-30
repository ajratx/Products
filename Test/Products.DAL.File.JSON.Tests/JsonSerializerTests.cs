namespace Products.DAL.File.JSON.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Products.Business.Entities;
    using Products.DAL.File.Interfaces;

    [TestClass]
    public class JsonSerializerTests
    {
        private const string TestJsonFile = "C:\\Shared\\test.json";

        [TestMethod]
        public void FileNotExists()
        {
            if (File.Exists(TestJsonFile)) File.Delete(TestJsonFile);

            var settings = Mock.Of<ISerializerSettings>();
            settings.FilePath = TestJsonFile;

            var serializer = new JsonSerializer<Product>(settings);
            var products = new List<Product>
                               {
                                   new Product { Name = "Product #1", Price = 1.23M, Count = 3 },
                                   new Product { Name = "Product #2", Price = 4.56M, Count = 8 }
                               };

            serializer.SerializeAsync(products).Wait();

            products = new List<Product> { new Product { Name = "Product #3", Price = 9M, Count = 5 } };

            serializer.SerializeAsync(products).Wait();

            products = serializer.DeserializeAsync().Result.ToList();

            serializer.Dispose();

            Assert.AreEqual(3, products.Count);
        }

        [TestMethod]
        public void MultiThread()
        {
            if (File.Exists(TestJsonFile)) File.Delete(TestJsonFile);

            var settings = Mock.Of<ISerializerSettings>();
            settings.FilePath = TestJsonFile;

            var serializer = new JsonSerializer<Product>(settings);
            var products = new List<Product>
                               {
                                   new Product { Name = "Product #1", Price = 1.23M, Count = 3 },
                                   new Product { Name = "Product #2", Price = 4.56M, Count = 8 }
                               };

            Parallel.Invoke(() => serializer.Dispose(), () => serializer.SerializeAsync(new Product[] {}));
        }
    }
}
