using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Product_API.Exceptions;

namespace Product_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string message = string.Empty;

            switch (ex)
            {
                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;
                case ConflictException:
                    statusCode = StatusCodes.Status409Conflict;
                    message = ex.Message;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "An unexpected error occurred. Please contact helpdesk.";
                    break;
            }
            _logger.LogError(ex, "Unhandled Exception for Path {Path} at {Timestamp}", context.Request.Path, DateTime.UtcNow);
            
            var errorResponse = new
                {
                    message = message,
                    timestamp = DateTime.UtcNow,
                    path = context.Request.Path
                };
                var jsonResponse = JsonSerializer.Serialize(errorResponse);
                
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
        }
    }
}