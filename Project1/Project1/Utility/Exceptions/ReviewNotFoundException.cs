namespace ReviewShelf.Utility.Exceptions;

[Serializable]
public class ReviewNotFoundException : Exception
{
    public ReviewNotFoundException()
    {
    }

    public ReviewNotFoundException(string? message) : base(message)
    {
    }

    public ReviewNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}