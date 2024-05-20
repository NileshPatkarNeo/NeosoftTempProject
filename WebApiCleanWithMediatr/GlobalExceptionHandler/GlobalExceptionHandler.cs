namespace WebApiCleanWithMediatr.GlobalExceptionHandler
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An unhandled exception occurred: {ex}");

                // Set the status code to 500 Internal Server Error
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Return a generic error message
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }

    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
