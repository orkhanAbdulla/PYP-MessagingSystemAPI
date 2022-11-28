using MessagingSystemApp.Application.Common.Behaviorus;
using MessagingSystemApp.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MessagingSystemApp.Api.Filters
{
    public class ApiExceptionFilterAttribute:ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>()
            {
                {typeof(NotFoundException),HandleNotFoundException },
                {typeof(ValidationException),HandleValidationException },
                {typeof(ForbiddenAccessException),HandleForbiddenAccessExceptionException },
                {typeof(UnauthorizedAccessException),HandleUnauthorizedAccessException },
                {typeof(BadRequestException),HandleBadRequestException },
                {typeof(ExpiredException),HandleExpiredException},
                {typeof(FileFormatException),HandleFileFormatException},
            };
        }
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }
        private void HandleException(ExceptionContext context)
        {
            Type type=context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }
        }
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = (NotFoundException)context.Exception;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleBadRequestException(ExceptionContext context)
        {
            var exception = (BadRequestException)context.Exception;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Bad Request",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Detail=context.Exception.Message,
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            context.ExceptionHandled = true;
        }
        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleForbiddenAccessExceptionException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }
        private void HandleExpiredException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status406NotAcceptable,
                Title = "Expired",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Detail=context.Exception.Message

            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status406NotAcceptable
            };

            context.ExceptionHandled = true;
        }
        private void HandleFileFormatException(ExceptionContext context)
        {
            var exception = (FileFormatException)context.Exception;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "FileFormatException",
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
