using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarioFood.Models;
using Moq;

namespace MarioFoodTests
{
    [TestClass]
    public class ReviewTest
    {
        [TestMethod]
        public void ReviewModelTest()
        {
            var review = new Review();

            review.ReviewContentBody = "Nice Food";
            var result = review.ReviewContentBody;

            Assert.AreEqual("Nice Food", result);
        }


    }
}