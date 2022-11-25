using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Abstractions.Repositories;
using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Abstractions.Services.MailServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices;
using MessagingSystemApp.Application.Abstractions.Services.StorageServices.Base;
using MessagingSystemApp.Application.Abstractions.Services.TokenServices;
using MessagingSystemApp.Application.Abstracts.Common;
using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Domain.Identity;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using MessagingSystemApp.Infrastructure.Persistence.Identity;
using MessagingSystemApp.Infrastructure.Persistence.Interceptors;
using MessagingSystemApp.Infrastructure.Persistence.Repositories;
using MessagingSystemApp.Infrastructure.Services.MailServices;
using MessagingSystemApp.Infrastructure.Services.StorageServices;
using MessagingSystemApp.Infrastructure.Services.StorageServices.Base;
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
            serviceCollection.AddScoped<AuditableEntitySaveChangesInterceptor>();
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
            serviceCollection.AddScoped<IEmployeeChannelRepository, EmployeeChannelRepository>();
            //AddContainerServices
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IStorageService, StorageService>();
           


            return serviceCollection;
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
    }
}
