using MessagingSystemApp.Application.Abstractions.Services.IdentityServices;
using MessagingSystemApp.Application.Common.Exceptions;
using MessagingSystemApp.Application.Common.Helpers;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Identity
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<Employee> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<IdentityResult> VerifyEmailConfirmationToken(Employee employee, string token)
        {
            token=token.UrlDecode();
            var result = await _userManager.ConfirmEmailAsync(employee, token);
            return result;
        }

        public async Task<bool> VerifyForgetPasswordAsync(Employee employee, string token)
        {
            token=token.UrlDecode();
            return await _userManager.VerifyUserTokenAsync(employee, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        }
        public async Task<Employee> GetUserAuthAsync()
        {
            string userName = _httpContextAccessor.HttpContext.User?.Identity?.Name ?? throw new BadRequestException();
            return await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        }

    }
}
