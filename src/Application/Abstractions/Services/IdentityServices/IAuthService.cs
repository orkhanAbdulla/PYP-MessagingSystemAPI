using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Services.IdentityServices
{
    public interface IAuthService
    {
        public Task<string> ForgetPassawordAsync(Employee employee);
        public Task<bool> VerifyForgetPasswordAsync(Employee employee, string token);
        public Task<string>GenerateEmailConfirmationTokenAsync(string Id);
        public Task<IdentityResult> VerifyEmailConfirmationToken(Employee employee,string token);
        public Task<Employee> GetUserAuthAsync();
    }
}
