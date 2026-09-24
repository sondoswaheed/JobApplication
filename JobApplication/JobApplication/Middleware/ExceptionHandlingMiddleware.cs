using System.Text.Json;

namespace JobApplication.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = 400;

                await WriteResponse(
                    context,
                    new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = 403;

                await WriteResponse(
                    context,
                    new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = 404;

                await WriteResponse(
                    context,
                    new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = 400;

                await WriteResponse(
                    context,
                    new { message = ex.Message });
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;

                await WriteResponse(
                    context,
                    new
                    {
                        message = "An unexpected error occurred."
                    });
            }
        }

        private static async Task WriteResponse( HttpContext context, object response)
        {
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}