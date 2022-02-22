using System.Text.Json;
using BankApp.Exceptions;

namespace BankApp.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.StatusCode = 400;

            await WriteAsyncJson(context, badRequestException.Message);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;

            await WriteAsyncJson(context, notFoundException.Message);
        }
        catch
        {
            context.Response.StatusCode = 500;
            await WriteAsyncJson(context, "Something went wrong");
        }
    }

    private static async Task WriteAsyncJson(HttpContext context, string messageError)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(
            new
            {
                success = false,
                message = messageError
            }
        ));
    }
}