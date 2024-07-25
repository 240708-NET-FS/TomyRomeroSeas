using System;

namespace ReviewShelf.Utility.Exceptions
{
    [Serializable]
    public class BookReviewDeleteException : Exception
    {
        public BookReviewDeleteException()
        {
        }

        public BookReviewDeleteException(string? message) : base(message)
        {
        }

        public BookReviewDeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
