using CustomerDiscount.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.configuration
{
    public class DiscountCustomerBootstrapper
    {
        public static void Configure(IServiceCollection services , string connectionStrings)
        {
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionStrings));

        }
    }
}
