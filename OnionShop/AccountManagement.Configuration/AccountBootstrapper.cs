﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore.Repository;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Application;
using _0_Framework.Application;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Application.Contract.Role;
namespace AccountManagement.Configuration
{
    public class AccountBootstrapper
    {
        public static void Configure(IServiceCollection services , string connectionString)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddDbContext<AccountContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
