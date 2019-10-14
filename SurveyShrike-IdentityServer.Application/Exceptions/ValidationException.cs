using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 2:02:52 PM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }

    }
}
