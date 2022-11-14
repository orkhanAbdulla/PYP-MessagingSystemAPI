using MessagingSystemApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstracts.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Connection> Connections { get; }
        DbSet<EmployeeChannel> EmployeeChannels { get; }
        DbSet<Post> Posts { get; }
        DbSet<Reaction> Reactions { get; }
        DbSet<Attachment> Attachments { get; }
    }
}
