using Microsoft.AspNetCore.Mvc;

namespace BlazesoftChallenge_Ashran.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);
                await WriteProblemDetailsAsync(context, ex);
            }
        }

        private Task WriteProblemDetailsAsync(HttpContext context, Exception ex)
        {
            int statusCode;
            string title;

            switch (ex)
            {
                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Invalid request.";
                    break;

                case InvalidOperationException:
                    statusCode = StatusCodes.Status409Conflict;
                    title = "Invalid operation.";
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "Server error.";
                    break;
            }

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = ex.Message
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            return context.Response.WriteAsJsonAsync(problem);
        }
    }
}
