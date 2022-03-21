namespace BankApp.Exceptions.RequestExceptions;

public class NotFoundException : AppRequestException
{
    public NotFoundException(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}