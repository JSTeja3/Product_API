using System.Diagnostics;


namespace Product_API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var timeStamp = Stopwatch.StartNew();
            _logger.LogInformation($"[REQUEST] {context.Request.Method} {context.Request.Path} {DateTime.Now:hh:mm:ss tt}");

            await _next(context);

            timeStamp.Stop();

            _logger.LogInformation($"[RESPONSE] Status: {context.Response.StatusCode} | Duration: {timeStamp.ElapsedMilliseconds} ms");
    
        }
    }
}