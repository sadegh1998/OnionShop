using _01_ShopQuery.Contracts.Product;
using CommentMamagement.Infrastructure.EFCore;
using CommentManagement.ApplicationContract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetails(id);
        }
        public IActionResult OnPost(AddComment comment,string productSlug)
        {
            comment.Type = CommentType.Product;
            var result = _commentApplication.Add(comment);
            return RedirectToPage("/Product", new {Id = productSlug });
        }
    }
}
