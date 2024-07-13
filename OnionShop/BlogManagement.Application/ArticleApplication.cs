using _0_Framework.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Domain.ArtilceAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if(_articleRepository.Exisit(x=>x.Title == command.Title))
            {
                return operation.Failed(ApplicationMessages.Duplicate);   
            }
            var slug = command.Slug.Slugify();
            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var article = new Article(command.Title,command.ShortDescription,command.Description , picturePath, command.PictureAlt,command.PictureTitle,
                publishDate,slug,command.Keywords , command.MetaDescription ,command.CanonicalAddress,command.CategoryId);
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exisit(x => x.Title == command.Title && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            var article = _articleRepository.GetArticleWithCategory(command.Id);
            if(article == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            var slug = command.Slug.Slugify();
            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var path = $"{article.Category.Slug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            article.Edit(command.Title, command.ShortDescription, command.Description, picturePath, command.PictureAlt, command.PictureTitle,
                publishDate, slug, command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);
            _articleRepository.SaveChanges();
            return operation.Success();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
