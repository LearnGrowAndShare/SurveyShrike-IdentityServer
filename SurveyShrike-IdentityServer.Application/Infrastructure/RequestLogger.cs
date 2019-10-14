using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:08:03 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Infrastructure
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Identity Request: {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }
}
