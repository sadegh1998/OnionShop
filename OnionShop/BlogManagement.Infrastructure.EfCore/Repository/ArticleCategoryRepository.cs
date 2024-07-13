using _0_Framework.Application;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long,ArticleCategory> , IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel { 
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory { 
            Id = x.Id,
            Name = x.Name ,
            Description = x.Description,
            MetaDescription = x.MetaDescription,
            CanonicalAddress = x.CanonicalAddress,
            Keywords = x.Keywords,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ShowOrder = x.ShowOrder,
            Slug = x.Slug
            }).FirstOrDefault(x=>x.Id == id);
        }

        public string GetSlugBy(long id)
        {
            return _context.ArticleCategories.FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel { 
            Id = x.Id ,
            Name = x.Name,
            Description = x.Description,
            Picture = x.Picture ,
            ShowOrder = x.ShowOrder,
            CreationDate = x.CreationDate.ToFarsi(),
            ArticlesCount = 0
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            var result = query.OrderByDescending(x => x.ShowOrder).ToList();
            return result;
        }
    }
}
