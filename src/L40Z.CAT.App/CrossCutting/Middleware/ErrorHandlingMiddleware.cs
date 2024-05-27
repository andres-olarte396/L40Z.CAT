using Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace CrossCutting.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions and return a JSON response with the error message.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">
        /// The next middleware in the pipeline.
        /// </param>
        /// <param name="logger">
        /// The logger to log exceptions.
        /// </param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">
        /// The current HTTP context.
        /// </param>
        /// <returns>
        /// A task that represents the completion of the middleware.
        /// </returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and returns a JSON response with the error message.
        /// </summary>
        /// <param name="context">
        /// The current HTTP context.
        /// </param>
        /// <param name="exception">
        /// The exception to handle.
        /// </param>
        /// <returns>
        /// A task that represents the completion of the middleware.
        /// </returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    status = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;
                case Core.Exceptions.ValidationException validationException:
                    status = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(validationException.Errors);
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "An unexpected error has occurred.";
                    break;
            }

            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = message }));
        }
    }
}
