using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest
{
    public class UpdateConnectionCommandRequest:IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
