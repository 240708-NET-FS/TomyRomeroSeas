using System.Text.RegularExpressions;
using System.Transactions;
using ReviewShelf.Entities;
using ReviewShelf.Service;
using ReviewShelf.Utility;

namespace ReviewShelf.Controller;

public class StartUpController{
    private readonly UserService _userService;
    private readonly LoginService _loginService;
    private readonly AccountService _accountService;
    private readonly BookReviewService _bookReviewService;
    private readonly MainMenuController _mainMenuController;

    public StartUpController (UserService userService , LoginService loginService, AccountService accountService, MainMenuController mainMenuController, BookReviewService bookReviewService)
    {
        _userService = userService;
        _loginService = loginService;
        _accountService = accountService;
        _bookReviewService = bookReviewService;
        _mainMenuController = mainMenuController;
   
    }

    public void Start()
    {
   
        State.isActiveStartUp = true;

        while(State.isActiveStartUp)
        {
            Console.Clear();
            Console.WriteLine("Welcome to ReviewShelf!");
            Console.WriteLine("------------------------");
            Console.WriteLine("");
            Console.WriteLine("1. Create an Account");
            Console.WriteLine("");
            Console.WriteLine("2. Login");
            Console.WriteLine("");
            Console.WriteLine("3. Book Reviews");
            Console.WriteLine("");
            Console.WriteLine("4. Exit");
            Console.WriteLine("");
           
            string? choice = Console.ReadLine();

             switch (choice)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    AllReviews();
                    break;
                case "4":
                    Quit();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    State.WaitForUser();
                    break;
            }
        }
    }

private void Register()
{
    Console.Clear();
    Console.WriteLine("Register For Review Shelf:");
    Console.WriteLine("------------------------");
    Console.WriteLine("Create Account");
    Console.WriteLine("------------------------");
    Console.WriteLine("(or press 0 to return to the main menu):");
    Console.WriteLine("------------------------");

    while(true)
    {
    // Get and validate first name
    string? firstName;
    while (true)
    {
        Console.Write("Enter your first name:");
        firstName = Console.ReadLine();
        if (firstName == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if (Validation.ValidateName(firstName))
            break;
        
        Console.WriteLine("Invalid first name. Please try again.");
    }

    // Get and validate last name
    string? lastName;
    while (true)
    {
        Console.WriteLine("------------------------");
        Console.Write("Enter your last name:");
        lastName = Console.ReadLine();
        if (lastName == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if (Validation.ValidateName(lastName))
            break;
        Console.WriteLine("Invalid last name. Please try again.");
    }

    // Get and validate username
    string? username;
    while (true)
    {
        Console.WriteLine("------------------------");
        Console.WriteLine("Username must be between 3 and 20 characters long and cannot be empty.");
        Console.Write("Enter username:");
        username = Console.ReadLine();
        if (username == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if (Validation.ValidateUsername(username))
            break;
        Console.WriteLine("Invalid username. Please try again.");
    }

    // Get and validate password
    string? password;
    while (true)
    {
        Console.WriteLine("------------------------");
        Console.Write("Enter password: ");
        password = Console.ReadLine();
        if (password == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if(Validation.ValidatePassword(password))
            break;
        Console.WriteLine("Invalid Password. Please try again.");
    }

    string? confirmPassword;
    while(true)
    {
        Console.WriteLine("------------------------");
        Console.Write("Confirm password: ");
        confirmPassword = Console.ReadLine();
        if (password == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if (confirmPassword == password)
            break;

        Console.WriteLine("Invalid Password Confirmation. Please try again.");
    }
    
        try
        {
        _accountService.CreateUserWithLogin(Validation.CapitalizeFirstLetter(firstName), Validation.CapitalizeFirstLetter(lastName), username, password);
        Console.WriteLine("------------------------");
        Console.WriteLine("Account created successfully!");
        Console.WriteLine($"Welcome { Validation.CapitalizeFirstLetter(firstName)}  {lastName} , username: {username}");
        State.WaitForUser();
        break;
        }
        
        catch (Exception ex)
        {
        Console.WriteLine($"Error creating account: {ex.Message}");
        State.WaitForUser();;
        break;          
        }
 
    }

    }

    private void AllReviews()
    {
    Console.Clear();
    Console.WriteLine("Review Shelf:");
    Console.WriteLine("------------------------");
    Console.WriteLine("View User Created Reviews");
    Console.WriteLine("------------------------");
    
    try{
    var reviews = _bookReviewService.GetAll();

            if (reviews == null || reviews.Count == 0)
                {
                    Console.WriteLine("No Reviews Found.");
                    Console.WriteLine();
                }
                else
                {
                    foreach (var review in reviews)
                    {
                        Console.WriteLine(review.ToStringUser());
                        Console.WriteLine("------------------------");
                    }
                }
    }catch(Exception ex)
    {
        Console.WriteLine("Error Fetching Book Reviews, Please Try Again.", ex);
        Console.WriteLine("------------------------");
    }
        State.WaitForUser();
    }

  private void Login()
{
    Console.Clear();
    Console.WriteLine("Review Shelf:");
    Console.WriteLine("------------------------");
    Console.WriteLine("Login");
    Console.WriteLine("------------------------");
    Console.WriteLine("(or press 0 to return to the main menu):");
    Console.WriteLine("------------------------");


    string? username;
    while (true)
    {
        Console.Write("Enter username: ");
        username = Console.ReadLine();
        if (username == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if (Validation.ValidateUsername(username))
            break;
        Console.WriteLine("Invalid username. Please try again.");
    }

    string? password;
    while (true)
    {
        Console.Write("Enter password: ");
        password = Console.ReadLine();
        if (password == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return; 
        }

        if (Validation.ValidatePassword(password))
            break;
        Console.WriteLine("Invalid password. Please try again.");
    }

    try
    {
        bool isAuthenticated = _loginService.Authenticate(username, password);
        if (isAuthenticated)
        {
            // Save User to State
            _userService.SetUserState(username);
            // Proceed to the main menu
            Console.WriteLine("------------------------");
            Console.WriteLine("Login successful!");
            State.WaitForUser();
            State.isActiveStartUp = false;
             _mainMenuController.ShowMenu();
            
        }
        else
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Login failed. Invalid username or password.");
            State.WaitForUser();
        }
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during login: {ex.Message}");
        State.WaitForUser();
    }
}


    private void Quit()
    {
        
        Console.WriteLine("------------------------");
        Console.WriteLine("Do you want to quit? Type Y or N");

        var input = Console.ReadLine();

         switch(input)
            {
                case "y":
                case "Y":
                    Console.Clear();
                    Console.WriteLine("Thank you for using ReviewShelf");
                    Console.WriteLine("");
                    Console.WriteLine("....Powering OFF");
                    State.isActiveStartUp = false;
                    break;
                case "N":
                case "n":
                    break;
                case null:
                default: 
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Invalid choice. Please try again.");
                    State.WaitForUser();
                    Quit();
                    break;
            }           
           
    }

  
}