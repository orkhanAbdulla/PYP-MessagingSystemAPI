using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Exceptions
{
    public class ValidationException:Exception
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException() : base("One or more validation failures have occurred")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failurGroup => failurGroup.Key, failurGroup => failurGroup.ToArray());
        }
        public ValidationException(IdentityResult failures) : this()
        {
            Errors = failures.Errors.GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(failurGroup => failurGroup.Key, failurGroup => failurGroup.ToArray());
        }
    }
}
