using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Entities
{
    public class Attachment:BaseEntity<int>
    {
        public Post Post { get; set; } = null!;
        public int PostId { get; set; }
        public string FileName { get; set; } = null!;
        public FileType FileType { get; set; }
    }
}
