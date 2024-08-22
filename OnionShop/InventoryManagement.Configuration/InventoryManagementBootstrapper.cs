using _0_Framework.Infrstructure;
using _01_ShopQuery.Contracts.Inventory;
using _01_ShopQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.ApplicationContract.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.InfrastructureEFCore;
using InventoryManagement.InfrastructureEFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration
{
    public class InventoryManagementBootstrapper
    {
       public static void Configuration(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            services.AddTransient<IInventoryQuery, InventoryQuery>();
            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
