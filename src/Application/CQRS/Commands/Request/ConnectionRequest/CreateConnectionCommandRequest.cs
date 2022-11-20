using MediatR;
using MessagingSystemApp.Application.Common.Mappings;
using MessagingSystemApp.Application.CQRS.Commands.Response.ConnectionResponse;
using MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest
{
    public class CreateConnectionCommandRequest:IRequest<CreateConnectionCommandResponse>
    {
        public string UserName { get; set; } = null!;
        public string? ReciverUserName { get; set; }
        public bool IsChannel { get; set; }
        public string? ChannelName { get; set; }
    }
}
