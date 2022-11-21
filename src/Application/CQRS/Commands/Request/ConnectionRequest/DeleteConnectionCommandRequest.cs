using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest
{
    public class DeleteConnectionCommandRequest:IRequest<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsChannel { get; set; }
    }
}
