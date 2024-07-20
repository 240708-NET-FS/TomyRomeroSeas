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

    public StartUpController (UserService userService , LoginService loginService, AccountService accountService)
    {
        _userService = userService;
        _loginService = loginService;
        _accountService = accountService;
    }

    public void Start()
    {
   
        State.isActive = true;

        while(State.isActive)
        {
            Console.Clear();
            Console.WriteLine("Welcome to ReviewShelf!");
            Console.WriteLine("------------------------");
            Console.WriteLine("");
            Console.WriteLine("1. Create an Account");
            Console.WriteLine("");
            Console.WriteLine("2. Login");
            Console.WriteLine("");
            Console.WriteLine("3. Exit");
            Console.WriteLine("");
           
            string? choice = Console.ReadLine();

             switch (choice)
            {
                case "1":
                    Console.WriteLine($"You chose: {choice}");
                    Register();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    Quit();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    WaitForUser();
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
    Console.WriteLine("(or press 1 to return to the main menu):");
    Console.WriteLine("------------------------");

    while(true)
    {
    // Get and validate first name
    string? firstName;
    while (true)
    {
        Console.Write("Enter your first name:");
        firstName = Console.ReadLine();
        if (firstName == "1")
        {
            Console.WriteLine("Returning to main menu...");
            WaitForUser();
            return; 
        }

        if (ValidateName(firstName))
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
        if (lastName == "1")
        {
            Console.WriteLine("Returning to main menu...");
            WaitForUser();
            return; 
        }

        if (ValidateName(lastName))
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
        if (username == "1")
        {
            Console.WriteLine("Returning to main menu...");
            WaitForUser();
            return; 
        }

        if (ValidateUsername(username))
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
        if (password == "1")
        {
            Console.WriteLine("Returning to main menu...");
            WaitForUser();
            return; 
        }

        if(ValidatePassword(password))
            break;
        Console.WriteLine("Invalid Password. Please try again.");
    }

    string? confirmPassword;
    while(true)
    {
        Console.WriteLine("------------------------");
        Console.Write("Confirm password: ");
        confirmPassword = Console.ReadLine();
        if (password == "1")
        {
            Console.WriteLine("Returning to main menu...");
            WaitForUser();
            return; 
        }

        if (confirmPassword == password)
            break;

        Console.WriteLine("Invalid Password Confirmation. Please try again.");
    }
    
        try
        {
        // _userService.CreateUser(firstName, lastName, username, password); // Implement this method in your UserService
        _accountService.CreateUserWithLogin(CapitalizeFirstLetter(firstName), CapitalizeFirstLetter(lastName), username, password);
        Console.WriteLine("------------------------");
        Console.WriteLine("Account created successfully!");
        Console.WriteLine($"Welcome { CapitalizeFirstLetter(firstName)}  {lastName} , username: {username}");
        WaitForUser();
        break;
        }
        
        catch (Exception ex)
        {
        Console.WriteLine($"Error creating account: {ex.Message}");
        WaitForUser();;
        break;          
        }
 
    }

    }

    private void Login()
    {
        Console.Clear();
        Console.WriteLine("Login");
    }

    private void WaitForUser()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
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
                    State.isActive = false;
                    break;
                case "N":
                case "n":
                    break;
                case null:
                default: 
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Invalid choice. Please try again.");
                    WaitForUser();
                    Quit();
                    break;
            }           
           
    }

     private bool ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return false;
            }
            if (name.Length < 2 || name.Length > 20)
            {
                Console.WriteLine("Name must be between 2 and 20 characters long.");
                return false;
            }
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s\-']+$"))
            {
                Console.WriteLine("Name can only contain letters, spaces, hyphens, and apostrophes.");
                return false;
            }
            return true;
        }

    private bool ValidateUsername(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username cannot be empty.");
            return false;
        }
        else if (username.Length < 3)
        {
            Console.WriteLine("Username must be at least 3 characters long.");
            return false;
        }
        else if (username.Length > 20)
        {
            Console.WriteLine("Username cannot be more than 20 characters long.");
            return false;
        }

        return true;
    }

    private bool ValidatePassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            return false;
        }
        else if (password.Length < 6)
        {
            Console.WriteLine("Password must be at least 6 characters long.");
            return false;
        }else if(password.Length > 20)
        {
            Console.WriteLine("Password cannot be more than 20 characters long.");
            return false;
        }

        return true;
    }
    

    public static string? CapitalizeFirstLetter(string? name)
{
    if (string.IsNullOrWhiteSpace(name))
    {
       return name;
    }

    return char.ToUpper(name[0]) + name.Substring(1);
}
}