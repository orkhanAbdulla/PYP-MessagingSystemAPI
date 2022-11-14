using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstractions.Services.MailServices
{
    public interface IMailService
    {
        Task<string> SendMailAsync(string toEmail, string subject, string htmlBody);
        public Task SendForgetPasswordMailAsync(string to, string userId, string token);
        public Task SendEmailConfirmationAsync(string to, string userId, string token);
    }
}
