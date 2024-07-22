using System;
using System.Text.RegularExpressions;

public class Validation
{

    public static bool ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("(Warning): Name cannot be empty.");
            return false;
        }
        if (name.Length < 2 || name.Length > 20)
        {
            Console.WriteLine("(Warning): Name must be between 2 and 20 characters long.");
            return false;
        }
        if (!Regex.IsMatch(name, @"^[a-zA-Z\s\-']+$"))
        {
            Console.WriteLine("(Warning): Name can only contain letters, spaces, hyphens, and apostrophes.");
            return false;
        }
        return true;
    }

    public static bool ValidateUsername(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("(Warning): Username cannot be empty.");
            return false;
        }
        if (username.Length < 3)
        {
            Console.WriteLine("(Warning): Username must be at least 3 characters long.");
            return false;
        }
        if (username.Length > 20)
        {
            Console.WriteLine("(Warning): Username cannot be more than 20 characters long.");
            return false;
        }

        return true;
    }

    public static bool ValidatePassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("(Warning): Password cannot be empty.");
            return false;
        }
        if (password.Length < 6)
        {
            Console.WriteLine("(Warning): Password must be at least 6 characters long.");
            return false;
        }
        if (password.Length > 20)
        {
            Console.WriteLine("(Warning): Password cannot be more than 20 characters long.");
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

     public static bool ValidateGenre(string? genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
        {
            Console.WriteLine("(Warning): Genre cannot be empty.");
            return false;
        }
        if (genre.Length < 2 || genre.Length > 30)
        {
            Console.WriteLine("(Warning): Genre must be between 2 and 30 characters long.");
            return false;
        }
        if (!Regex.IsMatch(genre, @"^[a-zA-Z\s\-']+$"))
        {
            Console.WriteLine("(Warning): Genre can only contain letters, spaces, hyphens, and apostrophes.");
            return false;
        }
        return true;
    }

    public static bool ValidateBookTitle(string? bookTitle)
    {
        if (string.IsNullOrWhiteSpace(bookTitle))
        {
            Console.WriteLine("(Warning): Book title cannot be empty.");
            return false;
        }
        if (bookTitle.Length < 2 || bookTitle.Length > 30)
        {
            Console.WriteLine("(Warning): Book title must be between 2 and 30 characters long.");
            return false;
        }
            if (!Regex.IsMatch(bookTitle, @"^[a-zA-Z0-9\s\-']+$"))
        {
            Console.WriteLine("(Warning): Book title can only contain letters, digits, spaces, hyphens, and apostrophes.");
            return false;
        }
        return true;
    }

    public static bool ValidateReviewText(string? reviewText)
    {
        if (string.IsNullOrWhiteSpace(reviewText))
        {
            Console.WriteLine("(Warning): Review text cannot be empty.");
            return false;
        }
        if (reviewText.Length < 10 || reviewText.Length > 1000)
        {
            Console.WriteLine("(Warning): Review text must be between 10 and 1000 characters long.");
            return false;
        }
        return true;
    }

}
