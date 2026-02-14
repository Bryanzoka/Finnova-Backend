using System.Text.Json;
using Finnova.Application.Exceptions;
using Finnova.Domain.Exceptions;
using FluentValidation;

namespace Finnova.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "unexpected error, try again later";
            object? errors = null;

            switch (ex)
            {
                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;

                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    message = ex.Message;
                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;
                    
                case DomainException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;

                case ForbiddenException:
                    statusCode = StatusCodes.Status403Forbidden;
                    message = ex.Message;
                    break;

                case InvalidDomainOperationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;

                case UnauthorizedDomainException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    message = ex.Message;
                    break;

                case ValidationException exception:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "validation failed";
                    errors = exception.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                    break;

                default:
                    _logger.LogError(ex, $"unexpected error: {ex.Message}");
                    break;
            }

            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                message,
                errors
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}