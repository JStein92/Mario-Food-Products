using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using MarioFood.Models;
using MarioFood.Controllers;
using System.Collections.Generic;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace MarioFood.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        EFProductRepository db = new EFProductRepository(new TestDbContext());
        EFReviewRepository reviewDb = new EFReviewRepository(new TestDbContext());

        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductName="Meatball", ProductCost="11", ProductCountry="Italy"},
                 new Product{ProductName="Fish", ProductCost="14", ProductCountry="Japan"},
                 new Product{ProductName="Cake", ProductCost="44", ProductCountry="France"}

            }.AsQueryable());
        }

        private void DeleteAll()
        {
            TestDbContext db = new TestDbContext();
            db.Products.RemoveRange(db.Products.ToList());
            db.SaveChanges();
        }

        [TestMethod]
        public void DB_CreateNewEntry_Test()
        {
            ProductController controller = new ProductController(db);
            Product testProduct = new Product();
            testProduct.ProductName = "Steve";

            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test()
        {
            DbSetup();
            ProductController controller = new ProductController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        [TestMethod]
        public void Mock_IndexListOfProducts_Test()
        {
            DbSetup();
            ViewResult indexView = new ProductController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_ProductDetail_Test()
        {
            DbSetup();
            Product testProduct = new Product()
            {
                ProductName = "Egg",
                ProductCountry = "Whatever!"
            };

            ViewResult detailView = new ProductController(mock.Object).Details(testProduct.ProductId) as ViewResult;
            var result = detailView.ViewData.Model;

            Assert.AreEqual(result, testProduct);


        }

        [TestMethod]
        public void Mock_ConfirmEntry_Test()
        {
            DbSetup();
            ProductController ProductController = new ProductController(mock.Object);
            Product testProduct = new Product();
            testProduct.ProductName = "Beef";
            testProduct.ProductCountry = "Whatever!";

            ViewResult indexView = ProductController.Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Product>;

            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void GetAverageRating()
        {
            ProductController controller = new ProductController(db);
            ReviewController reviewController = new ReviewController(reviewDb);

            Product testProduct = new Product()
            {
                ProductName = "Egg",
                ProductCountry = "USA!"
            };
            controller.Create(testProduct);

            Review testReview1 = new Review { ReviewRating = 2, ProductId = testProduct.ProductId};
            Review testReview2 = new Review { ReviewRating = 4, ProductId = testProduct.ProductId };

          
            reviewController.Create(testReview1);
            reviewController.Create(testReview2);

            var collection = new List<Review>();
            collection.Add(testReview1);
            collection.Add(testReview2);

            var reviewTotal = 0;
            var reviewCount = 0;

            for (int i = 0; i < collection.Count; i++)
            {
                reviewCount++;
                reviewTotal += collection[i].ReviewRating;
            }

            var avg = reviewTotal / reviewCount;

            Assert.AreEqual(avg, 3);
        }



        public void Dispose()
        {
            this.DeleteAll();
        }

    }

}