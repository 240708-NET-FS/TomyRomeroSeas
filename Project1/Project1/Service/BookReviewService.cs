using System.Transactions;
using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Service;
using ReviewShelf.Utility.Exceptions;

public class BookReviewService: IService<BookReview>{

    private readonly BookReviewDAO _bookReviewDao;

    public BookReviewService(BookReviewDAO  bookReviewDao)
    {
         _bookReviewDao = bookReviewDao;
    }

    public void Create(int? userId, string? bookTitle, string? genre, string? reviewText)
        {
            
            // Ensure userId has a value
            if (!userId.HasValue)
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null.");


            if (string.IsNullOrEmpty(bookTitle))
                throw new ArgumentNullException(nameof(bookTitle), "Book title cannot be null or empty.");
            
            if (string.IsNullOrEmpty(genre))
                throw new ArgumentNullException(nameof(genre), "Genre cannot be null or empty.");
            
            if (string.IsNullOrEmpty(reviewText))
                throw new ArgumentNullException(nameof(reviewText), "Review text cannot be null or empty.");

            using (var transaction = new TransactionScope())
            {
                try
                {
                    // Create the BookReview entity
                    var bookReview = new BookReview
                    {
                        BookTitle = bookTitle,
                        Genre = genre,
                        Review = reviewText,
                        UserId = userId.Value
                    };

                    // Save the BookReview
                    _bookReviewDao.Create(bookReview);

                    // Complete the transaction
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, etc.)
                    throw new BookReviewCreationException("An error occurred while creating the book review.", ex);
                }
            }
        }

    public void Delete(BookReview item)
    {
        throw new NotImplementedException();
    }

    public void Update(BookReview item)
    {
        throw new NotImplementedException();
    }

     public ICollection<BookReview> GetReviewsByUserId(int? userId)
    {
            // Ensure userId has a value
            if (!userId.HasValue)
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null.");

            // Call DAO method to get reviews by user ID
            return _bookReviewDao.GetReviewsByUserId(userId.Value);
    }

    public ICollection<BookReview> GetAll(){

        throw new NotImplementedException();
        
    }

}