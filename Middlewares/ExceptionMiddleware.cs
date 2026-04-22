using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
                _logger.LogError(ex, "Unhandled exception for Path {Path} at {Timestamp}", context.Request.Path, DateTime.UtcNow);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType  = "application/json";

                var errorResponse = new
                {
                    message = "An unexcepted error occured. Please contact helpdesk",
                    timestamp = DateTime.UtcNow,
                    path = context.Request.Path
                };

                 var jsonResponse = JsonSerializer.Serialize(errorResponse);

                 await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}