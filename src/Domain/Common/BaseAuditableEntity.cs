using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Common
{
    public abstract class BaseAuditableEntity:BaseEntity<int>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }

    }
}
