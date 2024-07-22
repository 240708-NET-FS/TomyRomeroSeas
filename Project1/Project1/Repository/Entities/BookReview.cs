namespace ReviewShelf.Entities;
public class BookReview
{
    public int BookReviewId { get; set; }
    public string BookTitle { get; set; } = "";
    public string Review { get; set; } = "";
    public string Genre { get; set; } ="";
    public int UserId { get; set; }
    public User? User { get; set; }

    public override string ToString()
    {
        return $"{BookReviewId}: {BookTitle} - {Review}";
    }
}
