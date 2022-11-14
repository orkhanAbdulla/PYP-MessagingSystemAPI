using FluentValidation;
using MessagingSystemApp.Application.CQRS.Commands.Request.UserRequest;
using MessagingSystemApp.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MessagingSystemApp.Application.CQRS.Validators.CommandValidators.EmployeeCommandValidator
{
    public class CreateEmployeeCommandRequestValidator:AbstractValidator<CreateEmployeeCommandRequest>
    {
        private readonly UserManager<Employee> _userManager;
        public CreateEmployeeCommandRequestValidator(UserManager<Employee> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Username).NotEmpty().NotNull().MaximumLength(50).WithMessage("Username cannot be more than 50 characters");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(customer => customer.Password).NotEmpty().NotNull().WithMessage("Please enter the password").Equal(customer => customer.PasswordConfirm);
           
        }

    }
}
