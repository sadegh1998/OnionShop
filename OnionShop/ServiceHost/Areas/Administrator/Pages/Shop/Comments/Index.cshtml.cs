using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.ApplicationContract.Comment;
using ShopManagement.ApplicationContract.Slide;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> comments{ get; set; }
        public CommentSearchModel searchModel { get; set; }
        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            comments = _commentApplication.Search(searchModel);
        }
        
       
        public IActionResult OnGetCancel(long id) { 
        var result = _commentApplication.Cancel(id);

            if (result.IsSuccedded)
               return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetConfirm(long id) {
        var result = _commentApplication.Confirm(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

    }
}
