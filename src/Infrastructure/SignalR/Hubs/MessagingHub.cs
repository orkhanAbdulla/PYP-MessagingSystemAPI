using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.SignalR.Hubs
{
    public class MessagingHub:Hub
    {
        private readonly UserManager<Employee> _userManager;

        public MessagingHub(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Employee employee = await _userManager.FindByNameAsync(Context.User.Identity.Name);
                employee.SignalRId = Context.ConnectionId;
                await _userManager.UpdateAsync(employee);
            }
        }
    }
}
