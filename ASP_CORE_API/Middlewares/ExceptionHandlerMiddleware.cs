namespace ASP_CORE_API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errId = Guid.NewGuid();
                // Log This Exception
                logger.LogError(ex, $"{errId} : {ex.Message}");

                // return a custom error response 
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errId,
                    ErrorMessage = "Something went wrong...",
                };

                httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
