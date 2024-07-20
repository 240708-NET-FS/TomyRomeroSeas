using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Service;

public class BookReviewService: IService<BookReview>{

    private readonly BookReviewDAO _bookReview;

    public BookReviewService(BookReviewDAO  bookReviewDao)
    {
         _bookReview = bookReviewDao;
    }

    public void Create(BookReview item){
        throw new NotImplementedException();
        
    }

    public void Delete(BookReview item)
    {
        throw new NotImplementedException();
    }

    public void Update(BookReview item)
    {
        throw new NotImplementedException();
    }

    public ICollection<BookReview> GetAll(){

        throw new NotImplementedException();
        
    }

}