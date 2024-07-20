using Microsoft.EntityFrameworkCore;
using ReviewShelf.Entities;

namespace ReviewShelf.DAO
{
    public class BookReviewDAO : IDAO<BookReview>
    {
        private readonly ApplicationDbContext _context;

        public BookReviewDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new BookReview
        public void Create(BookReview item)
        {
            // Ensure the User associated with the BookReview exists
            var userExists = _context.Users.Any(u => u.UserId == item.UserId);
            if (!userExists)
                throw new KeyNotFoundException($"User with ID {item.UserId} not found.");

            _context.BookReviews.Add(item);
            _context.SaveChanges();
        }

        // Delete a BookReview
        public void Delete(BookReview item)
        {
            var existingReview = _context.BookReviews
                                          .Include(br => br.User)
                                          .FirstOrDefault(br => br.BookReviewId == item.BookReviewId);

            if (existingReview == null)
                throw new KeyNotFoundException($"BookReview with ID {item.BookReviewId} not found.");

            _context.BookReviews.Remove(existingReview);
            _context.SaveChanges();
        }

        // Retrieve all BookReviews
        public ICollection<BookReview> GetAll()
        {
            return _context.BookReviews.Include(br => br.User).ToList();
        }

        // Retrieve a BookReview by its ID
        public BookReview GetById(int ID)
        {
            var review = _context.BookReviews
                                  .Include(br => br.User)
                                  .FirstOrDefault(br => br.BookReviewId == ID);

            if (review == null)
                throw new KeyNotFoundException($"BookReview with ID {ID} not found.");

            return review;
        }

        // Update an existing BookReview
        public void Update(BookReview newItem)
        {
            var existingReview = _context.BookReviews
                                          .Include(br => br.User)
                                          .FirstOrDefault(br => br.BookReviewId == newItem.BookReviewId);

            if (existingReview != null)
            {
                existingReview.BookTitle = newItem.BookTitle;
                existingReview.Review = newItem.Review;

                _context.BookReviews.Update(existingReview);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"BookReview with ID {newItem.BookReviewId} not found.");
            }
        }

        // Retrieve all BookReviews that belong to a specific User
        public ICollection<BookReview> GetReviewsByUserId(int userId)
        {
            // Check if the user exists
            var userExists = _context.Users.Any(u => u.UserId == userId);
            if (!userExists)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            // Retrieve all BookReviews for the specified User
            var reviews = _context.BookReviews
                                  .Where(br => br.UserId == userId)
                                  .Include(br => br.User)
                                  .ToList();

            return reviews;
        }
    }
}
