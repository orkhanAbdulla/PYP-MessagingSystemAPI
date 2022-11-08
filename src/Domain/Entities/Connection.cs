using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Entities
{
    public class Connection:BaseAuditableEntity
    {
        public Connection()
        {
            EmployeeChannels=new HashSet<EmployeeChannel>();
        }
        public string? Name { get; set; } 
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<EmployeeChannel> EmployeeChannels { get; set; }
        public string? SenderId { get; set; }
        public Employee? Sender { get; set; }
        public string? ReciverId { get; set; }
        public Employee? Reciver { get; set; }

    }
}
