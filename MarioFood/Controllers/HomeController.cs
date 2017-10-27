using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarioFood.Models;

namespace MarioFood.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepo;
        private IReviewRepository ReviewRepo = new EFReviewRepository();

        public HomeController(IProductRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.productRepo = new EFProductRepository();
            }
            else
            {
                this.productRepo = thisRepo;
            }
        }

        public IActionResult Index()
        {
            var mostRecentProducts = productRepo.Products.ToList
             ().OrderByDescending(x => x.ProductId).ToList();

 

            var threeMostRecentProducts = new List<Product>();

            for (int i = 0; i < mostRecentProducts.Count && i < 2; i++)
            {
                threeMostRecentProducts.Add(mostRecentProducts[i]);
            }

           var reviews = ReviewRepo.Reviews.ToList();

           var  productIdList = new List<int>();
            foreach(var review in reviews)
            {
                productIdList.Add(review.ProductId);
            }

            var mostReviewedIds = productIdList.GroupBy(i => i).OrderByDescending(grp => grp.Count()).ToList();

            var threeMostReviewedProductIds = new List<int>();

            for (int i = 0; i < mostReviewedIds.Count && i < 2; i++)
            {
                threeMostReviewedProductIds.Add(mostReviewedIds[i].Key);
            }

            var mostReviewedProducts = new List<Product>();

            for (int i = 0; i < threeMostReviewedProductIds.Count; i++)
            {
                mostReviewedProducts.Add(productRepo.Products.FirstOrDefault(Product => Product.ProductId == threeMostReviewedProductIds[i]));
            }

            ViewBag.MostReviewed = mostReviewedProducts;


            return View(threeMostRecentProducts);
        }

    }
}
