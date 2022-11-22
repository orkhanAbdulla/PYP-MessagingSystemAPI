using FluentValidation;
using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.QueryValidators.ConnectionQueryValidator
{
    public class GetDirectMessagesListByUserQueryRequestValidator:AbstractValidator<GetDirectMessagesListByUserQueryRequest>
    {
        public GetDirectMessagesListByUserQueryRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
        }
    }
}
