using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Models
{
    public class Result
    {
        public bool Successed { get; set; }
        public string[] Errors { get; set; }
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Successed = succeeded;
            Errors = errors.ToArray();
        }
        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
