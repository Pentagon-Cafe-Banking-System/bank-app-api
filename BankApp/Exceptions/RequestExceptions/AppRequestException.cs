using System.Text.Json;

namespace BankApp.Exceptions.RequestExceptions;

public abstract class AppRequestException : Exception
{
    protected AppRequestException(RequestError error) : base(JsonSerializer.Serialize(error))
    {
    }
}