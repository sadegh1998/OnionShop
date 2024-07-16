using _0_Framework.Application;
using _01_ShopQuery.Contracts.Article;
using _01_ShopQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArtilceAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ShopQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShowOrder = x.ShowOrder,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count
                }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var articleCategory = _blogContext.ArticleCategories.
                Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                ShowOrder=x.ShowOrder,
                ArticlesCount = x.Articles.Count,
                Articles = MapArticle(x.Articles)
                })
                .FirstOrDefault(x => x.Slug == slug);

            articleCategory.KeywordList = articleCategory.Keywords.Split(',').ToList();

            return articleCategory;
        }

        private static List<ArticleQueryModel> MapArticle(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel {
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}
