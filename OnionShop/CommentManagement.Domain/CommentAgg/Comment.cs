using _0_Framework.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string WebSite { get; private set; }
        public string Message { get; private set; }
        public bool IsCanceled { get; private set; }
        public bool IsConfirmed { get; private set; }
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }

        public long ParentId { get; private set; }
        public  Comment Parent { get; private set; }
        public  List<Comment> Children { get; private set; }
        
        public Comment(string name, string email,string webSite, string message , long ownerRecordId , int type,long parentId)
        {
            Name = name;
            Email = email;
            WebSite = webSite;
            Message = message;
            IsCanceled = false;
            IsConfirmed = false;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
        }
        public void Cancel()
        {
            IsCanceled = true;
            IsConfirmed = false;
        }
        public void Confirm()
        {
            IsConfirmed = true;
            IsCanceled= false;
        }
    }
}
