namespace Products.Client.WCF.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Products.Business.Entities;

    [TestClass]
    public class ProductClientTests
    {
        [TestMethod]
        public void TestMethod1()
        {

            var client = new ProductClient();

            client.AddAsync(new Product { Name = "Product #12", Price = 1.23m, Count = 8 }).Wait();

            var products = client.GetAllAsync().Result;
        }
    }
}
