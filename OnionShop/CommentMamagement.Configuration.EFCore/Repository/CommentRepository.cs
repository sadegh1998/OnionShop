using _0_Framework.Application;
using _0_Framework.Infrstructure;
using CommentMamagement.Infrastructure.EFCore;
using CommentManagement.ApplicationContract.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastracture.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
            {
                Id = x.Id,
                Email = x.Email,
                WebSite = x.WebSite,
                Name = x.Name,
                Message = x.Message,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                OwnerRecordId = x.OwnerRecordId,    
                Type = x.Type,
                CommentDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }
            var comments = query.OrderByDescending(x => x.Id).ToList();
            return comments;
        }
    }
}
