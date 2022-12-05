using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.EmployeeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.EmployeeCommandValidator
{
    public class RefreshTokenLoginCommandRequestValidator:AbstractValidator<RefreshTokenLoginCommandRequest>
    {
        public RefreshTokenLoginCommandRequestValidator()
        {
          
        }
    }
}
