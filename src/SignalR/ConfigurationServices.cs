using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.SignalR
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSignalR();
            return serviceCollection;
        }
    }
}
