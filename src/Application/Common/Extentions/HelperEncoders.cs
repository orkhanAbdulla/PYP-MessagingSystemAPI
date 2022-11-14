using MessagingSystemApp.Application.Common.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Helpers
{
    public static class HelperEncoders
    {
        public static string UrlEncode(this string value)
        {
            byte[] tokenBytes = Encoding.UTF8.GetBytes(value);
            return WebEncoders.Base64UrlEncode(tokenBytes);
        }
        public static string UrlDecode(this string value)
        {
            byte[] tokenBytes = WebEncoders.Base64UrlDecode(value);
             return Encoding.UTF8.GetString(tokenBytes);
        }
    }
}
