using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Common.Helpers;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Identity
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<Employee> _userManager;

        public AuthService(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> ForgetPassawordAsync(Employee employee)
        {
            string token =await _userManager.GeneratePasswordResetTokenAsync(employee);
            token=token.UrlEncode();
            return token;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string Id)
        {
            Employee employee = await _userManager.FindByIdAsync(Id);
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(employee);
            token = token.UrlEncode();
            return token;
        }

        public async Task<Result> VerifyEmailConfirmationToken(Employee employee, string token)
        {
            token=token.UrlDecode();
            var result = await _userManager.ConfirmEmailAsync(employee, token);
            return result.ToApplicationResult();
        }

        public async Task<bool> VerifyForgetPasswordAsync(Employee employee, string token)
        {
            token=token.UrlDecode();
            return await _userManager.VerifyUserTokenAsync(employee, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        }
        
    }
}
