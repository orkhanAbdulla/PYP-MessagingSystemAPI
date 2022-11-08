using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Domain.Identity;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using MessagingSystemApp.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ///AddContext
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),builderoptions=>builderoptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //AddIdentityLibrary
            serviceCollection.AddIdentity<Employee, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //AddContainer
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            serviceCollection.AddScoped<IConnectionRepository, ConnectionRepository>();
            serviceCollection.AddScoped<IPostRepository, PostRepository>();
            serviceCollection.AddScoped<IAttachmentRepository, AttachmentRepository>();
            serviceCollection.AddScoped<IReactionRepository, ReactionRepository>();

            return serviceCollection;
        }
    }
}
