using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            Posts=new HashSet<Post>();
        }
        public ICollection<EmployeeChannel> EmployeeChannels { get; set; }
        public ICollection<Post> Posts { get; set; }
        public string? Name { get; set; } 
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public string? SenderId { get; set; }
        public Employee? Sender { get; set; }
        public string? ReciverId { get; set; }
        public Employee? Reciver { get; set; }

    }
}
