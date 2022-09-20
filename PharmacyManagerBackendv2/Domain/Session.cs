namespace Domain;
public class Session
{
    public int Id { get; set; }
    public Guid Token { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}