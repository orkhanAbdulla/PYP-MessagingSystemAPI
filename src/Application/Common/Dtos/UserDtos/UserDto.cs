using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Dtos.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
    }
}
