using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace MarioFood.Models
{
    public class MarioFoodContext : IdentityDbContext<ApplicationUser>
    {
        public MarioFoodContext(DbContextOptions options) : base(options)
        {

        }

        public MarioFoodContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
 

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;Port=3306;database=mario-food;uid=root;pwd=root;");

    }

}

