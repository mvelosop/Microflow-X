using Domion.Base;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tenants.App.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : CommandResult, new()
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior(
            ILogger<ValidatorBehavior<TRequest, TResponse>> logger,
            IValidator<TRequest>[] validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Validating {RequestName} - validators: {Count}; value: {@RequestValue}", typeof(TRequest).Name, _validators.Length, request);

            var sw = new Stopwatch();

            sw.Start();

            List<ValidationFailure> failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            TResponse response;

            if (failures.Any())
            {
                _logger.LogWarning("Validated {RequestName} - failures: {@Failures}", typeof(TRequest).Name, failures);

                response = new TResponse { ValidationFailures = failures };
            }
            else
            {
                response = await next();
            }

            sw.Stop();

            _logger.LogDebug("Validated {RequestName} - elapsed time (ms): {Time:F3}; response: {@Response}", typeof(TRequest).Name, sw.Elapsed.TotalMilliseconds, response);

            return response;
        }
    }
}