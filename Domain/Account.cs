namespace Domain;

public class Account
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public Service? Service { get; set; }
}