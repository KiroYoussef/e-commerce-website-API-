using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Electronic.Api.Model
{
    public class ContextDB : IdentityDbContext<ApplicationUser>
    {


        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }
        public virtual DbSet<FvoriteProduct> FvoriteProducts { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<SatusHistory> SatusHistories { get; set; }


    }
}
