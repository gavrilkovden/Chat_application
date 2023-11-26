
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            SetErrorResponse(context, ex);

            var errorMessage = ex.Message;
            await context.Response.WriteAsync(errorMessage);
        }

        private void SetErrorResponse(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is ChatInvalidInputException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (ex is ChatNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }

}
