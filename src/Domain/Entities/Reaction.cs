using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Entities
{
    public class Reaction:BaseAuditableEntity
    {
        public Emoji Emoji { get; set; }
        public Post Post { get; set; } = null!;
        public int PostId { get;set; }
    }
}
