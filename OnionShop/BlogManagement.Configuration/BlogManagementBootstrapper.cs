using BlogManagement.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Domain.ArtilceAgg;
using BlogManagement.Infrastructure.EfCore;
using BlogManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleApplication, ArticleApplication>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
