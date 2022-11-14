using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Models
{
    public class Token
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
