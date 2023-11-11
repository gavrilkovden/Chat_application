
using ExceptionHandling.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Chat_application
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ChatInvalidInputException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errorMessage = ex.Message;
                await context.Response.WriteAsync(errorMessage);
            }
            catch (ChatNotFoundException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var errorMessage = ex.Message;
                await context.Response.WriteAsync(errorMessage);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorMessage = ex.Message;
                await context.Response.WriteAsync(errorMessage);

            }
        }
    }

}
