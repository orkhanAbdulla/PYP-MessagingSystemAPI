using FluentValidation;
using MessagingSystemApp.Application.CQRS.Queries.Request.ConnectionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Validators.QueryValidators.ConnectionQueryValidator
{
    public class GetChannelListByUserQueryRequestValidator:AbstractValidator<GetChannelListByUserQueryRequest>
    {
        public GetChannelListByUserQueryRequestValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
        }
    }
}
