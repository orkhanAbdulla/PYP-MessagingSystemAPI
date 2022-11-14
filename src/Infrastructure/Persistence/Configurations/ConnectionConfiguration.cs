using MessagingSystemApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Configuration
{
    public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasOne(x => x.Reciver).WithMany(x => x.Recivers).HasForeignKey(x => x.ReciverId);
            builder.HasOne(x => x.Sender).WithMany(x => x.Senders).HasForeignKey(x => x.SenderId);
        }
    }
}
