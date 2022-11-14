using MessagingSystemApp.Application.Abstractions.Services.MailServices;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Services.MailServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly ISendGridClient _sendGridClient;

        public MailService(IConfiguration configuration, ISendGridClient sendGridClient)
        {
            _configuration = configuration;
            _sendGridClient = sendGridClient;
        }

        public async Task SendForgetPasswordMailAsync(string to, string userId, string token)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br>If you have requested a new password, you can renew your password from the link below.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["ClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(token);
            mail.AppendLine("\">Please click here</a></strong><br><br><span style=\"font-size:12px;\">NOT :If you didn’t request this email, there’s nothing to worry about — you can safely ignore it.</span><br><br><br>MessaginSystemApp");
            await SendMailAsync(to, "Forget Password Reset", mail.ToString());
        }
        public async Task SendEmailConfirmationAsync(string to,string userId,string token)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["ClientUrl"]);
            mail.AppendLine("/Login/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(token);
            mail.AppendLine("\">Please check your email for the verification action.</a></strong><br><br><span style=\"font-size:12px;\">NOT :If you didn’t request this email, there’s nothing to worry about — you can safely ignore it.</span><br><br><br>MessaginSystemApp");
            await SendMailAsync(to, "Email Confirmation", mail.ToString());
        }
        public async Task<string> SendMailAsync(string toEmail, string subject, string htmlBody)
        {
            string fromEmail = _configuration["SendGridEmailSetting:FromEmail"];
            string fromName = _configuration["SendGridEmailSetting:FromName"];
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject=subject,
                HtmlContent= htmlBody,
            };
            msg.AddTo(toEmail);

            var response=await _sendGridClient.SendEmailAsync(msg);
           return response.IsSuccessStatusCode ? "Email Send" : "Email Sending failed";
        }
    }
}
