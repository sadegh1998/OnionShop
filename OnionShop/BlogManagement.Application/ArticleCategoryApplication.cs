using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategory;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategory, IFileUploader fileUploader)
        {
            _articleCategory = articleCategory;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if(_articleCategory.Exisit(x=>x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            var slug = command.Slug.Slugify();
            var picturePath = _fileUploader.Upload(command.Picture , slug);
            var articleCategory = new ArticleCategory(command.Name,command.Description,command.ShowOrder,picturePath,command.PictureAlt,command.PictureTitle,slug,command.Keywords,command.MetaDescription,command.CanonicalAddress);
            _articleCategory.Create(articleCategory);
            _articleCategory.SaveChanges();
            return operation.Success();

        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();


            if (_articleCategory.Exisit(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            var articleCategory = _articleCategory.Get(command.Id);

            if (articleCategory == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            var slug = command.Slug.Slugify();
            var picturePath = _fileUploader.Upload(command.Picture, slug);
            articleCategory.Edit(command.Name, command.Description, command.ShowOrder, picturePath, command.PictureAlt, command.PictureTitle, slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _articleCategory.SaveChanges();
            return operation.Success();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategory.GetArticleCategories();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategory.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategory.Search(searchModel);
        }
    }
}
