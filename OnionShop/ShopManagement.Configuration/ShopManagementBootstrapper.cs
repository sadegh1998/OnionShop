using _0_Framework.Infrstructure;
using _01_ShopQuery.Contracts.Order;
using _01_ShopQuery.Contracts.Product;
using _01_ShopQuery.Contracts.ProductCategory;
using _01_ShopQuery.Contracts.Slide;
using _01_ShopQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.ApplicationContract.ProductCategory;
using ShopManagement.ApplicationContract.ProductPicture;
using ShopManagement.ApplicationContract.Slide;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProiductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastracture.EFCore;
using ShopManagement.Infrastracture.EFCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configuration(IServiceCollection services , string connectionString)
        {
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductRepository,ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();
            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();



            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
