using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Domain.Entities
{
    public class EmployeeChannel:BaseEntity<int>
    {
        public Connection Channel { get; set; } = null!;
        public int ChannelId { get; set; }
        public Employee Employee { get; set; } = null!;
        public string EmployeeId { get; set; }=null!;
    }
}
