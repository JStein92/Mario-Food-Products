using Microsoft.AspNetCore.Mvc;
using MarioFood.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MarioFood.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepo;

        public ProductController(IProductRepository thisRepo = null)
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
            return View(productRepo.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product Product)
        {
            productRepo.Save(Product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int ProductId)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(Product => Product.ProductId == ProductId);
            return View(thisProduct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int ProductId)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(Product => Product.ProductId == ProductId);
            productRepo.Remove(thisProduct);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int ProductId)
        {
            var thisProduct = productRepo.Products
                   .Include(x => x.Reviews)
                   .FirstOrDefault(x => x.ProductId == ProductId);

            return View(thisProduct);
        }

        public IActionResult Update(int ProductId)
        {
            var thisProduct = productRepo.Products.FirstOrDefault((Product => Product.ProductId == ProductId));
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Update(Product Product)
        {

            productRepo.Edit(Product);
            return RedirectToAction("Index");
        }

    }
}