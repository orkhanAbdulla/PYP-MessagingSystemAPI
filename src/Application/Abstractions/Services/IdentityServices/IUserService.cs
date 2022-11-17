

using MessagingSystemApp.Application.Common.Dtos.UserDtos;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace MessagingSystemApp.Application.Abstractions.Identity
{
    public interface IUserService
    {
        
        Task<Employee> GetUserAsync(Expression<Func<Employee, bool>>? expression=null);
        Task<SignInResult> CheckPasswordSignInAsync(Employee employee,string password);
        Task<bool> ChekCurrentPasswordAsync(Employee employee, string password);
        bool CheckPasswordsIsSame(Employee employee,string newPassword);
        Task<IdentityResult> UpdatePasswordAsync(Employee employee,string token,string newPassword);
        public Task<IdentityResult> ChangePasswordAsync(Employee employee, string currentPassword, string newPassword);
        Task<string> GetUserNameAsync(string userId);
        Task<(IdentityResult IdentityResult, string UserId)> CreateUserAsync(string Username, string Email, string password);
        Task UpdateRefreshToken(Employee employee, string refreshToken,DateTime accessTokenDate,int addOnAccessTokenDate);
        Task<IEnumerable<UserDto>> GetAll();
    }
}
