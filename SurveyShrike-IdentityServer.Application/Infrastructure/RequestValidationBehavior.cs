
using FluentValidation;
using MediatR;
using SurveyShrike_IdentityServer.Application.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = SurveyShrike_IdentityServer.Application.Exceptions.ValidationException;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:09:48 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Infrastructure
{

    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}
