using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using MessagingSystemApp.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Repositories
{
    public class EmployeeChannelRepository:Repository<EmployeeChannel,int>,IEmployeeChannelRepository
    {
        public EmployeeChannelRepository(ApplicationDbContext context):base(context)
        {
        }

        public async Task<bool> IsExistChannelNameAsync(string EmployeeId, string channelName)
        {
            return await Table.Where(x => x.EmployeeId == EmployeeId).AllAsync(y => y.Channel.Name!=null && y.Channel.Name.ToLower() != channelName.ToLower());
        }
    }
}
