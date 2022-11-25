using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Entities
{
    public class Post:BaseAuditableEntity
    {
        public Post()
        {
            ReplyPosts = new HashSet<Post>();
        }
        public string Message { get; set; } = null!;
        public Connection Connection { get; set; } = null!;
        public int ConnectionId { get; set; }
        public Employee Employee { get; set; }=null!;
        public string EmployeeId { get; set; } = null!;
        public bool IsEdited { get; set; }
        public bool IsReply { get; set; }
        public Post? ReplyPost { get; set; }
        public int? ReplyPostId { get; set; }
        public ICollection<Post> ReplyPosts { get; set; }
    }
}
