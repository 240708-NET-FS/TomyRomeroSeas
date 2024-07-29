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

  

    public BookReview Update(int bookreviewId, int? userId, string bookTitle, string genre, string reviewText)
    {
        try{
        BookReview result = GetBookReviewById(bookreviewId, userId);

        result.BookTitle = bookTitle;
        result.Genre = genre;
        result.Review = reviewText;

        _bookReviewDao.Update(result);

        return result;
        }catch(Exception ex){
            throw new BookReviewCreationException("Failed to Update Book Review", ex);
        }
    }

    public void Delete(int bookreviewId, int? userId)
    {
        try{
            BookReview result = GetBookReviewById(bookreviewId, userId);
            _bookReviewDao.Delete(result);

        }catch(Exception ex)
        {
            throw new BookReviewCreationException($"Failed to delete Book Review", ex);
        }
       
    }

    public BookReview GetBookReviewById(int bookreviewId, int? userId)
    {
        // Ensure params have a value
        if (!userId.HasValue)
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null.");

        try
        {
        BookReview review =  _bookReviewDao.GetById(bookreviewId);

        // Check if the book review belongs to the specified user
        if (review.UserId != userId)
            throw new UnauthorizedAccessException("You do not have access to this review. Please try another ID");

        return review;

        }catch(Exception ex)
        {
            throw new ReviewNotFoundException("Failed to get Book Review", ex);
        }

        
    }

     public ICollection<BookReview> GetReviewsByUserId(int? userId)
    {
        try{
             // Ensure userId has a value
            if (!userId.HasValue)
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null.");

            // Call DAO method to get reviews by user ID
            return _bookReviewDao.GetReviewsByUserId(userId.Value);
        }catch(Exception ex)
        {
            throw new Exception("Failed to get book reveiw by user ID", ex);
        }

    }

    public ICollection<BookReview> GetAll(){

        try{
            return _bookReviewDao.GetAll();
        }catch(Exception ex)
        {
            throw new Exception("Failed to get book reviews.", ex);
        }
        
        
    }

}