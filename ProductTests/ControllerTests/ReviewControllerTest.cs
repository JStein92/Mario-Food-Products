using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using MarioFood.Models;
using MarioFood.Controllers;
using System.Collections.Generic;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace MarioFoodTests
{
    [TestClass]
    public class ReviewControllerTest : IDisposable
    {
        EFReviewRepository db = new EFReviewRepository(new TestDbContext());

        Mock<IReviewRepository> mock = new Mock<IReviewRepository>();
        private void DbSetup()
        {
            mock.Setup(m => m.Reviews).Returns(new Review[]
            {
                new Review{ReviewRating=5, ProductId=1, ReviewAuthor = "Jon", ReviewContentBody = "Nice work"},
                new Review{ReviewRating=3, ProductId=1, ReviewAuthor = "Jill", ReviewContentBody = "bad work"},
                new Review{ReviewRating=1, ProductId=1, ReviewAuthor = "Bill", ReviewContentBody = "Great soup"},

            }.AsQueryable());
        }

        private void DeleteAll()
        {
            TestDbContext db = new TestDbContext();
            db.Reviews.RemoveRange(db.Reviews.ToList());
            db.SaveChanges();
        }

        [TestMethod]
        public void DB_CreateNewEntry_Test()

        {
            // Arrange
            ReviewController controller = new ReviewController(db);
            Review testReview = new Review();
            testReview.ReviewContentBody = "TestDb Review Name";
            testReview.ProductId = 1;

            // Act
            controller.Create(testReview);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

            // Assert
            CollectionAssert.Contains(collection, testReview);
        }


        [TestMethod]
        public void Mock_GetViewResultIndex_Test()
        {
            DbSetup();
            ReviewController controller = new ReviewController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexListOfReviews_Test()
        {
            DbSetup();
            ViewResult indexView = new ReviewController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Review>));
        }

        [TestMethod]
        public void Mock_ReviewDetail_Test()
        {
            DbSetup();
            Review testReview = new  Review { ReviewRating = 5, ProductId = 1, ReviewAuthor = "Jon", ReviewContentBody = "Nice work" };

            ViewResult detailView = new ReviewController(mock.Object).Details(testReview.ReviewId) as ViewResult;
            var result = detailView.ViewData.Model;

            Assert.AreEqual(result, testReview);
        }

        [TestMethod]
        public void Mock_ReviewRatingRange_Test()
        {
            DbSetup();
            Review testReview = new Review { ReviewRating = 0, ProductId = 1, ReviewAuthor = "Jon", ReviewContentBody = "Nice work" };

            Review testReview2 = new Review { ReviewRating = 3, ProductId = 1, ReviewAuthor = "Jon", ReviewContentBody = "Nice work" };

            var allReviews = new List<Review> { testReview,testReview2};

            var validReviews = new List<Review>();

            foreach(var review in allReviews)
            {
                if (review.ReviewRating > 0 && review.ReviewRating <=5)
                {
                    validReviews.Add(review);
                }
            }
            

            CollectionAssert.AreEqual(validReviews, new List<Review> { testReview2});
        }

        [TestMethod]
        public void Mock_ReviewBodyRange_Test()
        {
            DbSetup();
            Review testReview = new Review { ReviewRating = 0, ProductId = 1, ReviewAuthor = "Jon", ReviewContentBody = "Nice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice workNice work" }; //too many chars

            Review testReview2 = new Review { ReviewRating = 3, ProductId = 1, ReviewAuthor = "Jon", ReviewContentBody = "Nice workNice workNice workNice workNice workNice workNice workNice workNice workNic" }; //84 chars

            var allReviews = new List<Review> { testReview, testReview2 };

            var validReviews = new List<Review>();

            foreach (var review in allReviews)
            {
                if (review.ReviewContentBody.Length >= 50 && review.ReviewContentBody.Length <= 250)
                {
                    validReviews.Add(review);
                }
            }


            CollectionAssert.AreEqual(validReviews, new List<Review> { testReview2 });
        }

        [TestMethod]
        public void Mock_ConfirmEntry_Test()
        {
            DbSetup();
            ReviewController controller = new ReviewController(mock.Object);
            Review testReview = new Review { ReviewRating = 5, ProductId = 1, ReviewAuthor = "Jon", ReviewContentBody = "Nice work" };

            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Review>;

            CollectionAssert.Contains(collection, testReview);
        }


        [TestMethod]
        public void Db_Delete_Test()
        {
            ReviewController controller = new ReviewController(db);
            Review testReview = new Review();
            testReview.ReviewAuthor = "TestDb Review body";
            testReview.ProductId = 1;

            controller.Create(testReview);
            controller.DeleteConfirmed(testReview.ReviewId);

            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

            CollectionAssert.DoesNotContain(collection, testReview);
        }

        public void Dispose()
        {
            this.DeleteAll();
        }
    }
}