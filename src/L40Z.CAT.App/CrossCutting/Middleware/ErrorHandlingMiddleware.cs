using Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace CrossCutting.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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
