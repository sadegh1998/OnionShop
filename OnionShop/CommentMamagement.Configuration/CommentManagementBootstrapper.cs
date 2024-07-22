using CommentMamagement.Infrastructure.EFCore;
using CommentManagement.Application;
using CommentManagement.ApplicationContract.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastracture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace CommentManagement.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configuration(IServiceCollection services , string connectionString)
        {
            
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();


            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
