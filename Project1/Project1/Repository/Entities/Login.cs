namespace ReviewShelf.Entities;

public class Login
{
    public int LoginId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public override string ToString()
    {
        return $"{LoginId} {Username} {Password} {UserId} {User}";
    }
}
