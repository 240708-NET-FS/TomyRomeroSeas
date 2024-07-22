using System;

namespace ReviewShelf.Utility.Exceptions
{
    [Serializable]
    public class BookReviewCreationException : Exception
    {
        public BookReviewCreationException()
        {
        }

        public BookReviewCreationException(string? message) : base(message)
        {
        }

        public BookReviewCreationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
