using System.Diagnostics;


namespace Product_API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var timeStamp = Stopwatch.StartNew();
            Console.WriteLine($"[REQUEST] {context.Request.Method} {context.Request.Path} {DateTime.Now:hh:mm:ss tt}");

            await _next(context);

            timeStamp.Stop();

            Console.WriteLine($"[RESPONSE] Status: {context.Response.StatusCode} | Duration: {timeStamp.ElapsedMilliseconds} ms");
    
        }
    }
}