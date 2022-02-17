using BankApp.Exceptions;
using System.Text.Json;

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
        catch // (Exception e)
        {
            context.Response.StatusCode = 500; 
            await WriteAsyncJson(context, "Something went wrong");
        }
       
    }

    private async Task WriteAsyncJson(HttpContext context, string messageError)
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