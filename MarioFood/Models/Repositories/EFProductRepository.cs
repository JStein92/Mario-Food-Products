using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarioFood.Models
{
    public class EFProductRepository : IProductRepository
    {
        MarioFoodContext db = new MarioFoodContext();

        public EFProductRepository(MarioFoodContext connection = null)
        {
            if (connection == null)
            {
                this.db = new MarioFoodContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public IQueryable<Product> Products
        { get { return db.Products; } }

        public Product Edit(Product Product)
        {
            db.Entry(Product).State = EntityState.Modified;
            db.SaveChanges();
            return Product;
        }

        public void Remove(Product Product)
        {
            db.Products.Remove(Product);
            db.SaveChanges();
        }

        public Product Save(Product Product)
        {
            db.Products.Add(Product);
            db.SaveChanges();
            return Product;
        }
    }
}