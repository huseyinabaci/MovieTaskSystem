using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskSystem.Infrastructure.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally in the application.
    /// Logs errors and returns appropriate HTTP responses based on the exception type.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="loggerFactory">Factory for creating loggers.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("LogManuel");
        }

        /// <summary>
        /// Represents the structure of the exception response returned to the client.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        /// <param name="description">A description of the error.</param>
        public record ExceptionResponse(HttpStatusCode statusCode, string description);

        /// <summary>
        /// Invokes the middleware to process the HTTP request and handle exceptions.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);

                if (context.Response.StatusCode >= 400)
                {
                    string statusCode = context.Response.StatusCode.ToString();
                    string path = context.Request.Path.ToString();
                    _logger.LogError($"Error occurred status code: {statusCode} path: {path}");
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handles exceptions by logging the error and returning an appropriate HTTP response.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            ExceptionResponse response = exception switch
            {
                ApplicationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
                KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.statusCode;

            var json = System.Text.Json.JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json).ConfigureAwait(false);
        }
    }
}