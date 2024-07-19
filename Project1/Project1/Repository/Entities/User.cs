namespace ReviewShelf.Entities;
public class User
{
    
    public int UserId { get; set; }
  
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
   
    public string? UserName { get; set; }

    public  Login? Login { get; set; }

    public ICollection<BookReview>? BookReviews { get; set; }
    
    public override string ToString()
    {
        return $"{UserId} {FirstName} {LastName}";
    }
}
