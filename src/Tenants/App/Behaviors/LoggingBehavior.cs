﻿using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tenants.App.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            _logger.LogInformation("Handling {RequestName}; value: {@RequestValue}", typeof(TRequest).Name, request);

            try
            {
                var sw = new Stopwatch();

                sw.Start();
                TResponse response = await next();
                sw.Stop();

                _logger.LogInformation("Handled {RequestName}; elapsed time (ms): {Time:F3}; value: {@Response}", typeof(TRequest).Name, sw.Elapsed.TotalMilliseconds, response);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "{ExceptionType} handling {RequestName} (InnerException)", ex.InnerException.GetType().Name, typeof(TRequest).Name);

                return default(TResponse);
            }
        }
    }
}