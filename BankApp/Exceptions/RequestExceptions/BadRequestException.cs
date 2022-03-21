namespace BankApp.Exceptions.RequestExceptions;

public class BadRequestException : AppRequestException
{
    public BadRequestException(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}