using MessagingSystemApp.Application.Abstractions.Identity;
using MessagingSystemApp.Application.Common.Dtos.UserDtos;
using MessagingSystemApp.Application.Common.Helpers;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MessagingSystemApp.Infrastructure.Persistence.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public UserService(UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result> ChangePasswordAsync(Employee employee, string currentPassword, string newPassword)
        {
            var result=await _userManager.ChangePasswordAsync(employee, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(employee);
            }
            return result.ToApplicationResult();
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(Employee employee, string password)
        {
           return await _signInManager.CheckPasswordSignInAsync(employee, password, false);
        }

        public bool CheckPasswordsIsSame(Employee employee, string newPassword)
        {
            PasswordVerificationResult checkPasswordsIsSame = _userManager.PasswordHasher.VerifyHashedPassword(employee, employee.PasswordHash, newPassword);
            if (checkPasswordsIsSame != PasswordVerificationResult.Success)
            {
                return false;
            }
            return true;
        }

        public Task<bool> ChekCurrentPasswordAsync(Employee employee, string password)
        {
            return _userManager.CheckPasswordAsync(employee, password);
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string Username, string Email, string password)
        {
            Employee employee = new()
            {
                UserName = Username,
                Email = Email,
                Fullname=Email.Substring(0, Email.IndexOf("@"))
               
            };
            var result = await _userManager.CreateAsync(employee, password);
            return (result.ToApplicationResult(), employee.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }
        public async Task<Result> DeleteUserAsync(Employee user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _userManager.Users.Select(u => new UserDto
            {
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();
        }

        public async Task<Employee> GetUserAsync(Expression<Func<Employee, bool>>? expression=null)
        {
            return expression is null ? await _userManager.Users.SingleOrDefaultAsync() : await _userManager.Users.SingleOrDefaultAsync(expression);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return String.Empty;
            }
            return user.UserName;
        }

        public async Task<Result> UpdatePasswordAsync(Employee employee, string token, string newPassword)
        {
            token= token.UrlDecode();
            var result= await _userManager.ResetPasswordAsync(employee, token, newPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(employee);
            }
            return result.ToApplicationResult();
        }

    }
}
