
using System.Net;
using ILogger = SampleWebApi.Shared.Logging.ILogger;

namespace SampleWebApi.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception has occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "Internal server error occurred. Please try again later."
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
