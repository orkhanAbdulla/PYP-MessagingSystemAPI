using MessagingSystemApp.Application.Common.Models;
using MessagingSystemApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Services.TokenServices
{
    public interface ITokenService
    {
        public Token GenerateAccessToken(Employee user, int second);
        public string GenerateRefreshToken();
    }
}
