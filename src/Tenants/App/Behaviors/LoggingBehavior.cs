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

            _logger.LogInformation("{RequestName} [Handling] - request: {@Request}", typeof(TRequest).Name, request);

            try
            {
                var sw = new Stopwatch();

                sw.Start();
                TResponse response = await next();
                sw.Stop();

                _logger.LogInformation("{RequestName} [Handled] ({Time:F3} ms) - response: {@Response}", typeof(TRequest).Name, sw.Elapsed.TotalMilliseconds, response);

                return response;
            }
            catch (Exception e)
            {
                Exception ex = e.InnerException ?? e;

                _logger.LogError(ex, "{ExceptionType} handling {RequestName}", ex.GetType().Name, typeof(TRequest).Name);

                return default(TResponse);
            }
        }
    }
}