using Microsoft.AspNetCore.Mvc;
using MarioFood.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MarioFood.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewRepository ReviewRepo = new EFReviewRepository();
        
        private IProductRepository productRepo = new EFProductRepository();
        public ReviewController(IReviewRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.ReviewRepo = new EFReviewRepository();
            }
            else
            {
                this.ReviewRepo = thisRepo;
            }
        }

        public IActionResult Index()
        {
            return View(ReviewRepo.Reviews.ToList());
        }

        public IActionResult Create(int ProductId)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(Product => Product.ProductId == ProductId);
            ViewBag.product = thisProduct;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review Review)
        {
            ReviewRepo.Save(Review);
            return RedirectToAction("Details", "Product", new {ProductId = Review.ProductId});
        }

        public IActionResult Delete(int ReviewId)
        {
            var thisReview = ReviewRepo.Reviews.FirstOrDefault(Review => Review.ReviewId == ReviewId);
            return View(thisReview);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int ReviewId)
        {
            var thisReview = ReviewRepo.Reviews.FirstOrDefault(Review => Review.ReviewId == ReviewId);
            ReviewRepo.Remove(thisReview);
            return RedirectToAction("Details", "Product", new { ProductId = thisReview.ProductId });
        }

        public IActionResult Details(int ReviewId)
        {
            var thisReview = ReviewRepo.Reviews
            .FirstOrDefault(x => x.ReviewId == ReviewId);

            return View(thisReview);
        }

        public IActionResult Update(int ReviewId)
        {
            var thisReview = ReviewRepo.Reviews.FirstOrDefault((Review => Review.ReviewId == ReviewId));
            return View(thisReview);
        }

        [HttpPost]
        public IActionResult Update(Review Review)
        {

            ReviewRepo.Edit(Review);
            return RedirectToAction("Index");
        }

    }
}