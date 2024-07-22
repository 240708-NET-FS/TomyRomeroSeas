using ReviewShelf.Controller;
using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Utility;

namespace ReviewShelf;

public class Program{
    private static ApplicationManager? _applicationManager;
    public static void Main(string [] args)
    {
        
        using (var context = new ApplicationDbContext())
            {
                _applicationManager = new ApplicationManager(context);
                _applicationManager.Start();
            }
    }
}

  public class ApplicationManager
    {
        private readonly UserDAO _userDao;
        private readonly LoginDAO _loginDao;
        private readonly BookReviewDAO _bookReviewDao;

        private readonly UserService _userService;
        private readonly LoginService _loginService;
        private readonly AccountService _accountService;
        private readonly BookReviewService _bookReviewService;

        private StartUpController _startUpController;
        private MainMenuController _mainMenuController;

        public ApplicationManager(ApplicationDbContext context)
        {
            _userDao = new UserDAO(context);
            _loginDao = new LoginDAO(context);
            _bookReviewDao = new BookReviewDAO(context);

            _userService = new UserService(_userDao);
            _loginService = new LoginService(_loginDao);
            _accountService = new AccountService(_userDao, _loginDao);
            _bookReviewService = new BookReviewService(_bookReviewDao);

            _mainMenuController = new MainMenuController(_bookReviewService, this);
            _startUpController = new StartUpController(_userService, _loginService, _accountService, _mainMenuController);
        }

        public void Start()
        {
            _startUpController.Start();
        }

        public void Restart()
        {
            Start();
        }
    }