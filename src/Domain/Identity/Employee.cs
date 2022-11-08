using MessagingSystemApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Identity
{
    public class Employee:IdentityUser
    {
        public Employee()
        {
            EmployeeChannels = new HashSet<EmployeeChannel>();
            Senders = new HashSet<Connection>();
            Recivers=new HashSet<Connection>();
            Posts = new HashSet<Post>();
        }
        public string Fullname { get; set; } = null!;
        public string? SignalRId { get; set; }
        public ICollection<EmployeeChannel> EmployeeChannels { get; set; }
        public ICollection<Connection> Senders { get; set; }
        public ICollection<Connection> Recivers { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
