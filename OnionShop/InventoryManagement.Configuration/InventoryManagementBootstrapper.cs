using _0_Framework.Infrstructure;
using InventoryManagement.Application;
using InventoryManagement.ApplicationContract.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.InfrastructureEFCore;
using InventoryManagement.InfrastructureEFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Configuration
{
    public class InventoryManagementBootstrapper
    {
       public static void Configuration(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
