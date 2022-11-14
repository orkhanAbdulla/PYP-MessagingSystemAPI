using MessagingSystemApp.Application.Common.Dtos.UserDtos;
using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Identity
{
    public interface IUserService
    {

        Task<Employee> GetUserAsync(Expression<Func<Employee, bool>>? expression=null);
        Task<SignInResult> CheckPasswordSignInAsync(Employee employee,string password);
        Task<bool> ChekCurrentPasswordAsync(Employee employee, string password);
        bool CheckPasswordsIsSame(Employee employee,string newPassword);
        Task<Result> UpdatePasswordAsync(Employee employee,string token,string newPassword);
        public Task<Result> ChangePasswordAsync(Employee employee, string currentPassword, string newPassword);
        Task<string> GetUserNameAsync(string userId);
        Task<(Result Result, string UserId)> CreateUserAsync(string Username, string Email, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<IEnumerable<UserDto>> GetAll();
    }
}
