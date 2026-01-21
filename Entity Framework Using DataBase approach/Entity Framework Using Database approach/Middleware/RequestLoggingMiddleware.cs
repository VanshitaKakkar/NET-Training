namespace Entity_Framework_Using_DataBase_approach.Middleware
{
    using System.Security.Claims;

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next,
                                        ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var userName = context.User.Identity.Name ?? "Anonymous";
            var path = context.Request.Path;
            var method = context.Request.Method;

            _logger.LogInformation(
                "HTTP {Method} {Path} started | UserName: {UserName}",
                method,
                path,
                userName
            );

            await _next(context);

            _logger.LogInformation(
                "HTTP {Method} {Path} completed | StatusCode: {StatusCode} | UserName: {UserName}",
                method,
                path,
                context.Response.StatusCode,
                userName
            );
        }
    }

}
