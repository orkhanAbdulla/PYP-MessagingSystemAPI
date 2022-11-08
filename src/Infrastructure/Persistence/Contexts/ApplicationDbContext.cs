using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<Employee>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
        public DbSet<Connection> Connections => Set<Connection>();

        public DbSet<EmployeeChannel> EmployeeChannels => Set<EmployeeChannel>();

        public DbSet<Post> Posts => Set<Post>();

        public DbSet<Reaction> Reactions => Set<Reaction>();

        public DbSet<Attachment> Attachments => Set<Attachment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
