using _0_Framework.Domain;
using CommentManagement.ApplicationContract.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long , Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);

    }
}
