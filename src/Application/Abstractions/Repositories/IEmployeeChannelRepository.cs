using MediatR;
using MessagingSystemApp.Application.Abstracts.Repositories.Base;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Repositories
{
    public interface IEmployeeChannelRepository:IRepository<EmployeeChannel,int>
    {
        public Task<bool> IsExistChannelNameAsync(string EmployeeId, string channelName);
    }
}
