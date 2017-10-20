using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarioFood.Models;
using Moq;

namespace MarioFoodTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void ProductModelTest()
        {
            var product = new Product();

            product.ProductName = "Meatball";
            var result = product.ProductName;

            Assert.AreEqual("Meatball", result);
        }
    }
}