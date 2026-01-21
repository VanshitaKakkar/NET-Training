namespace Entity_Framework_Using_DataBase_approach.Middleware
{
    using System.Net;
    using System.Security.Claims;

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next,
                                         ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var userName = context.User.Identity.Name ?? "Anonymous";

                _logger.LogError(
                    ex,
                    "Unhandled exception | Path: {Path} | UserName: {UserName}",
                    context.Request.Path,
                    userName
                );

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    "An unexpected error occurred. Please try again later."
                );
            }
        }
    }

}
