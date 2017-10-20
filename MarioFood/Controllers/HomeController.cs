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
            var mostRecentProducts = productRepo.Products.ToList().OrderByDescending(x => x.ProductId).ToList();

            var threeMostRecentProducts = new List<Product> { mostRecentProducts[0], mostRecentProducts[1], mostRecentProducts[2] };

           /* var mostReviewedProducts = ReviewRepo.Reviews.ToList();

           var  productIdList = new List<int>();
            foreach(var review in mostReviewedProducts)
            {
                productIdList.Add(review.ProductId);
            }

            var mostReviewedIds = productIdList.GroupBy(i => i).OrderByDescending(grp => grp.Count()).ToList();

            */


            return View(threeMostRecentProducts);
        }

    }
}
