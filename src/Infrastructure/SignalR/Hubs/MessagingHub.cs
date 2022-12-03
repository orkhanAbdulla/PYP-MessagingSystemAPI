using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.SignalR.Hubs
{
    [Authorize]
    public class MessagingHub:Hub
    {
        private readonly UserManager<Employee> _userManager;
        public MessagingHub(UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
        }

        public async override Task OnConnectedAsync()
        {
            if (Context.User?.Identity?.IsAuthenticated != null)
            {
                Employee employee = await _userManager.FindByNameAsync(Context.User?.Identity?.Name);
                employee.SignalRId = Context.ConnectionId;
                await _userManager.UpdateAsync(employee);
                await Clients.All.SendAsync("UserConnected", employee.UserName);
            }
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User?.Identity?.IsAuthenticated != null)
            {
                var employee = await _userManager.FindByNameAsync(Context.User?.Identity?.Name);
                employee.SignalRId = null;

                await _userManager.UpdateAsync(employee);
                await Clients.All.SendAsync("UserDisConnected", employee.UserName);
            }
        }
    }
}
