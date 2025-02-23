namespace Domain;

public class User
{
    public Guid Id { get; set; }
    public long TelegramId { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string? Password { get; set; }
    public bool IsSubscription { get; set; }
    
    public UserState? State { get; set; }
    public List<Service> Services { get; set; } = [];
}