using System.Text.Json;

namespace BankApp.Exceptions.RequestExceptions;

public abstract class AppRequestException : Exception
{
    protected AppRequestException(string name, string description) :
        base(JsonSerializer.Serialize(new RequestError(name, description)))
    {
    }
}