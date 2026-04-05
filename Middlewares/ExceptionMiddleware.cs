using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Product_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] message: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType  = "application/json";

                var errorResponse = new
                {
                    message = "An unexcepted error occured. Please contact helpdesk",
                    timestamp = DateTime.Now,
                    path = context.Request.Path
                };

                 var jsonResponse = JsonSerializer.Serialize(errorResponse);

                 await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}