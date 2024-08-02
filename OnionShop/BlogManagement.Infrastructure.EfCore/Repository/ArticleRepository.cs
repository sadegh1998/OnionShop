using _0_Framework.Application;
using _0_Framework.Infrstructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArtilceAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public Article GetArticleWithCategory(long id)
        {
            return _blogContext.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public EditArticle GetDetails(long id)
        {
            return _blogContext.Articles
               .Select(x => new EditArticle
               {
                   Id = x.Id,
                   Title = x.Title,
                   Description = x.Description,
                   ShortDescription = x.ShortDescription,
                   Keywords = x.Keywords,
                   MetaDescription = x.MetaDescription,
                   PictureAlt = x.PictureAlt,
                   PictureTitle = x.PictureTitle,
                   PublishDate = x.PublishDate.ToFarsi(),
                   Slug = x.Slug,
                   CategoryId = x.CategoryId,
                   CanonicalAddress = x.CanonicalAddress
               }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _blogContext.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                Title = x.Title,
                ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 50)) + " ...",
                CategoryId = x.CategoryId,
                Category = x.Category.Name
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if(searchModel.CategoryId > 0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            var result = query.OrderByDescending(x => x.Id).ToList();
            return result;
        }


    }
}
