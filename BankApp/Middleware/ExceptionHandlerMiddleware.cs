using System.Net;
using System.Text.Json;
using BankApp.Exceptions.RequestExceptions;

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
                _ => (int) HttpStatusCode.InternalServerError
            };

            try
            {
                var errorList = JsonSerializer.Deserialize<dynamic>(error.Message);
                await response.WriteAsJsonAsync(new {succeeded = false, message = errorList});
            }
            catch (JsonException)
            {
                await response.WriteAsJsonAsync(new {succeeded = false, message = error.Message});
            }
        }
    }
}