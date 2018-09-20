namespace Products.DAL.EF.SQLite
{
    using System.Data.SQLite;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SqliteContextTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var connection = new SQLiteConnection("Data Source=Products.sqlite;Version=3;");
            var context = new SqliteContext(connection);
        }
    }
}