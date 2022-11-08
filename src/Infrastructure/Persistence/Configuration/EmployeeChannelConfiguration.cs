using MessagingSystemApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Configuration
{
    public class EmployeeChannelConfiguration : IEntityTypeConfiguration<EmployeeChannel>
    {
        public void Configure(EntityTypeBuilder<EmployeeChannel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(x => x.Channel).WithMany(x => x.EmployeeChannels).HasForeignKey(x => x.ChannelId);
            builder.HasOne(x=>x.Employee).WithMany(x=>x.EmployeeChannels).HasForeignKey(x=>x.EmployeeId);
        }
    }
}
