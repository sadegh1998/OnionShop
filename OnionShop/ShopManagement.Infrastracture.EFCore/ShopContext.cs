using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProiductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastracture.EFCore.Mapping;

namespace ShopManagement.Infrastracture.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ShopContext(DbContextOptions<ShopContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCatagoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
