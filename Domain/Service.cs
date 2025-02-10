namespace Domain;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Description { get; set; }
    
    public User? User { get; set; }

    public List<Account> Accounts { get; set; } = [];
}