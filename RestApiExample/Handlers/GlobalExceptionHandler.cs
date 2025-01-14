using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiExample.Handlers
{
    //NET8
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private const string defaultContentType = "application/problem+json";
        private const string defaultReasonPhrase = "Unhandled Exception";
        private const string defaultType = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        private const int defaultStatus = StatusCodes.Status500InternalServerError;

        private static readonly JsonSerializerOptions defaultJsonSerializerOptions = new(JsonSerializerDefaults.Web)
        {
            //NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            // AllowTrailingCommas = false,
            //WriteIndented = true
        };

        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) //original
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is null)
                return true;

            try
            {
                ProblemDetails problemDetails = CreateProblemDetails(httpContext, exception);

                string problemDetailsJson = JsonSerializer.Serialize(problemDetails, defaultJsonSerializerOptions);

                _logger.LogError(exception, problemDetailsJson);

                //response         
                httpContext.Response.ContentType = defaultContentType;
                httpContext.Response.StatusCode = defaultStatus;

                await httpContext.Response.WriteAsync(problemDetailsJson, cancellationToken); 
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An exception has occurred in 'GlobalExceptionHandler':[{ex.Message}]");
            }

            // Return false to continue with the default behavior
            // - or - return true to signal that this exception is handled
            //return false;
            return true;
        }

        private static ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
        {
            string? traceId = Activity.Current?.Id;
            string traceIdentifier = httpContext.TraceIdentifier;

            int statusCode = httpContext.Response.StatusCode;
            
            string reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
                reasonPhrase = defaultReasonPhrase;

            string instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";

            ProblemDetails problemDetails = new()
            {
                Type = defaultType,
                Title = reasonPhrase,
                Status = statusCode,
                Detail = exception.Message,
                Instance = instance,
                Extensions = {
                    { "TraceId", traceId },
                    { "TraceIdentifier", traceIdentifier },
                    { "Timestamp", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ffff") },
                }
            };

            return problemDetails;
        }
    }
}