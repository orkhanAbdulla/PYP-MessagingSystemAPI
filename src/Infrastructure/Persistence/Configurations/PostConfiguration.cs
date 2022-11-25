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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x=>x.Message).HasMaxLength(1500);
            builder.HasOne(x => x.Employee).WithMany(x => x.Posts).HasForeignKey(x => x.EmployeeId);
            builder.HasOne(x => x.Connection).WithMany(x => x.Posts).HasForeignKey(x => x.ConnectionId);
            builder.HasOne(x => x.ReplyPost).WithMany(x => x.ReplyPosts).HasForeignKey(x => x.ReplyPostId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
