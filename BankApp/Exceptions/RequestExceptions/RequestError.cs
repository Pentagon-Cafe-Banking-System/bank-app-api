namespace BankApp.Exceptions.RequestExceptions;

public class RequestError
{
    public RequestError(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}