using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.ApplicationContract.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ArticleCategories
{
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel { get; set; }
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }
        private readonly IArticleCategoryApplication _articleCategory;

        public IndexModel(IArticleCategoryApplication articleCategory)
        {
            _articleCategory = articleCategory;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategory.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategory.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var articleCategory = _articleCategory.GetDetails(id);
            return Partial("./Edit", articleCategory);
        }
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategory.Edit(command);
            return new JsonResult(result);

        }
    }
}
