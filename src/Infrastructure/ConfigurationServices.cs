using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.MailServices;
using MessagingSystemApp.Application.Abstractions.Services.TokenServices;
using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Domain.Identity;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using MessagingSystemApp.Infrastructure.Persistence.Identity;
using MessagingSystemApp.Infrastructure.Persistence.Repositories;
using MessagingSystemApp.Infrastructure.Services.MailServices;
using MessagingSystemApp.Infrastructure.Services.TokenServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;


namespace MessagingSystemApp.Infrastructure
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ///AddContext
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),builderoptions=>builderoptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //AddIdentityLibrary
            serviceCollection.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //AddSendGrid
            serviceCollection.AddSendGrid(options =>
            options.ApiKey = configuration["SendGridEmailSetting:APIKey"]);
            //AddContainerRepositories
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            serviceCollection.AddScoped<IConnectionRepository, ConnectionRepository>();
            serviceCollection.AddScoped<IPostRepository, PostRepository>();
            serviceCollection.AddScoped<IAttachmentRepository, AttachmentRepository>();
            serviceCollection.AddScoped<IReactionRepository, ReactionRepository>();
            //AddContainerServices
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();


            return serviceCollection;
        }
    }
}
