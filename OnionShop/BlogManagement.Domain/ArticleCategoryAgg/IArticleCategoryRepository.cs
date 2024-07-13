using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long,ArticleCategory>
    {
        string GetSlugBy(long id);  
        EditArticleCategory GetDetails(long id);

        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
