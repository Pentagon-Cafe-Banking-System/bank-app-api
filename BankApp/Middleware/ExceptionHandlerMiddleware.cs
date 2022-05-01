using System.Net;
using BankApp.Exceptions;

namespace BankApp.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                BadRequestException => (int) HttpStatusCode.BadRequest,
                NotFoundException => (int) HttpStatusCode.NotFound,
                ForbiddenException => (int) HttpStatusCode.Forbidden,
                _ => (int) HttpStatusCode.InternalServerError
            };

            if (response.StatusCode == (int) HttpStatusCode.InternalServerError)
                await response.WriteAsJsonAsync(new {succeeded = false, error = "Internal server error"});
            else
                await response.WriteAsJsonAsync(new {succeeded = false, error = error.Message});
        }
    }
}