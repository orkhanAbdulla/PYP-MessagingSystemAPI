using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Exceptions
{
    public class ExpiredException:Exception
    {
        public ExpiredException() : base("$\"Entity \\\"Employee\\\" RefreshToken timed out\"") { }
        public ExpiredException(string message) : base(message) { }
        public ExpiredException(string message, Exception innerException) : base(message, innerException) { }
        public ExpiredException(string name, object key) : base($"Entity \"{name}\" {key} timed out") { }
    }
}
