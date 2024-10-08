﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.ApplicationContract.Comment
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string WebSite { get; set; }
        public long OwnerRecordId { get;  set; }
        public int Type { get;  set; }
        public long ParentId { get;  set; }
    }
}
