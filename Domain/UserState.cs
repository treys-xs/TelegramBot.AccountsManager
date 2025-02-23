namespace Domain;

public class UserState
{
    public Guid Id { get; set; }
    public int? Name { get; set; }
    public int? Step { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}