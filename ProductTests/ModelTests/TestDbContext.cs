using Microsoft.EntityFrameworkCore;

namespace MarioFood.Models
{
    public class TestDbContext : MarioFoodContext
    {
        public override DbSet<Product> Products { get; set; }
        public override DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=3306;database=mario-food_tests;uid=root;pwd=root;");
        }
    }
}