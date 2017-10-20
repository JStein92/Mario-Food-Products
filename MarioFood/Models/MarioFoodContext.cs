using Microsoft.EntityFrameworkCore;

namespace MarioFood.Models
{
    public class MarioFoodContext : DbContext
    {

        public MarioFoodContext()
        {

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;Port=3306;database=mario-food;uid=root;pwd=root;");

        public MarioFoodContext(DbContextOptions<MarioFoodContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}

