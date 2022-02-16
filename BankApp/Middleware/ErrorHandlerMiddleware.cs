using BankApp.Exceptions;
using System.Text.Json;

namespace BankApp.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var messageError = string.Empty;
        var success = false;

        try
        {
            await next.Invoke(context);
            success = true;
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.StatusCode = 400;
            messageError = badRequestException.Message;
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            messageError = notFoundException.Message;
        }
        catch // (Exception e)
        {
            context.Response.StatusCode = 500;
            messageError = "Something went wrong";
        }
        finally
        {
            if (!success)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        new
                        {
                            message = messageError
                        }));
            }
        }
    }
}