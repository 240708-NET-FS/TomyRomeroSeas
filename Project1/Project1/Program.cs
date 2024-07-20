using ReviewShelf.Controller;
using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Utility;

namespace ReviewShelf;

public class Program{
    public static void Main(string [] args)
    {
        using ( var context = new ApplicationDbContext())
        {
            UserDAO userDao = new UserDAO(context);
            LoginDAO loginDao = new LoginDAO(context);
            BookReviewDAO bookReviewDao = new BookReviewDAO(context);

            UserService userService = new UserService(userDao);
            LoginService loginService = new LoginService(loginDao);
            AccountService accountService = new AccountService(userDao, loginDao);
            BookReviewService bookReviewService = new BookReviewService(bookReviewDao);

            StartUpController startUpController = new StartUpController(userService, loginService, accountService);

            startUpController.Start();
            
        }
    }



}