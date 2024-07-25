using System;
using ReviewShelf.Service;
using ReviewShelf.Entities;
using ReviewShelf.Utility;
using ReviewShelf.Utility.Exceptions;

namespace ReviewShelf.Controller
{
    public class MainMenuController
    {
        private readonly BookReviewService _bookReviewService;
        private readonly ApplicationManager _applicationManager;

        public MainMenuController(BookReviewService bookReviewService, ApplicationManager applicationManager)
        {
            _bookReviewService = bookReviewService;
            _applicationManager = applicationManager;
        }

        public void ShowMenu()
        {
            if(State.currentUser != null)
            {
                State.isActiveMainMenu = true;
            }
            else{
                Console.Clear();
                Console.WriteLine("(Warning): No User is Logged in");
                State.WaitForUser();
                State.isActiveStartUp = true;
                return;
            }

            while (State.isActiveMainMenu)
            {
                Console.Clear();
                Console.WriteLine("Review Shelf:");
                Console.WriteLine("------------------------");
                Console.WriteLine($"Welcome {State.currentUser.UserName}!");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Create a Book Review");
                Console.WriteLine("");
                Console.WriteLine("2. Update a Book Review");
                Console.WriteLine("");
                Console.WriteLine("3. Delete a Book Review");
                Console.WriteLine("");
                Console.WriteLine("4. View My Book Reviews");
                Console.WriteLine("");
                Console.WriteLine("5. Logout");
                Console.WriteLine("");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateBookReview();
                        break;
                    case "2":
                        UpdateBookReview();
                        break;
                    case "3":
                        DeleteBookReview();
                        break;
                    case "4":
                        ViewBookReviews();
                        break;
                    case "5":
                        Logout();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        State.WaitForUser();
                        break;
                }
            }
        }

public void Logout(){
    Console.WriteLine("------------------------");
    Console.WriteLine("Do you want to Logout? Type Y or N");

        var input = Console.ReadLine();

         switch(input)
            {
                case "y":
                case "Y":
                    Console.Clear();
                    Console.WriteLine("Thank you for using ReviewShelf");
                    Console.WriteLine("");
                    Console.WriteLine("....Logging Out....");
                    Console.WriteLine("");
                    State.isActiveMainMenu = false;
                    State.currentUser = null;
                    State.WaitForUser();
                    _applicationManager.Restart();
                    break;
                case "N":
                case "n":
                    break;
                case null:
                default: 
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Invalid choice. Please try again.");
                    State.WaitForUser();
                    Logout();
                    break;
            }           
}

public void ConfirmDelete(int bookreviewId){
    Console.WriteLine("------------------------");
    Console.WriteLine("Are you sure you want to delete it? Type Y or N");

        var input = Console.ReadLine();

         switch(input)
            {
                case "y":
                case "Y":
                    Console.Clear();
                    try{
                    _bookReviewService.Delete(bookreviewId, State.currentUser?.UserId);
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Book Review Deleted Successfully");
                    State.WaitForUser();
                    }catch(Exception ex)
                    {
                        Console.WriteLine($"An error occured: {ex.Message}");
                    }
                    break;
                case "N":
                case "n":
                    break;
                case null:
                default: 
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Invalid choice. Please try again.");
                    State.WaitForUser();
                    ConfirmDelete(bookreviewId);
                    break;
            }  
}

private void CreateBookReview()
{
    Console.Clear();
    Console.WriteLine("Review Shelf:");
    Console.WriteLine("------------------------");
    Console.WriteLine("Create a Book Review:");
    Console.WriteLine("------------------------");
    Console.WriteLine("(or press 0 to return to the main menu):");
    Console.WriteLine("------------------------");

    // Get and validate book title
    string? bookTitle;
    while (true)
    {
        Console.Write("Enter book title: ");
        bookTitle = Console.ReadLine();
        if (bookTitle == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return;
        }
        if (Validation.ValidateBookTitle(bookTitle))
            break;
        Console.WriteLine("Invalid book title. Please try again.");
    }

    // Get and validate genre
    string? genre;
    while (true)
    {
        Console.Write("Enter genre: ");
        genre = Console.ReadLine();
        if (genre == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return;
        }
        if (Validation.ValidateGenre(genre))
            break;
        Console.WriteLine("Invalid genre. Please try again.");
    }

    // Get and validate review text
    string? reviewText;
    while (true)
    {
        Console.Write("Enter review: ");
        reviewText = Console.ReadLine();
        if (reviewText == "0")
        {
            Console.WriteLine("Returning to main menu...");
            State.WaitForUser();
            return;
        }
        if (Validation.ValidateReviewText(reviewText))
            break;
        Console.WriteLine("Invalid review text. Please try again.");
    }

    try
    {
        _bookReviewService.Create(State.currentUser?.UserId, bookTitle, genre, reviewText);
        Console.WriteLine("Book review created successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating book review: {ex.Message}");
    }

    State.WaitForUser();
}
       
private void UpdateBookReview()
{
    Console.Clear();
    Console.WriteLine("Review Shelf:");
    Console.WriteLine("------------------------");
    Console.WriteLine("Update Book Reviews:");
    Console.WriteLine("------------------------");   
    Console.WriteLine("(or press 0 to return to the main menu):");
    Console.WriteLine("------------------------");

    string? input;
    while (true)
                {
                    Console.WriteLine("Enter the ID of Book Review:");
                    input = Console.ReadLine();
                    if (input == "0")
                    {
                        Console.WriteLine("Returning to main menu...");
                        State.WaitForUser();
                        return;
                    }
                    if (Validation.ValidateInteger(input))
                        break;
                    Console.WriteLine("Invalid ID. Please try again.");
            }
            
            try
            {
                bool success = int.TryParse(input, out int id);

                if (success)
                {
                    BookReview bookReview = _bookReviewService.GetBookReviewById(id, State.currentUser?.UserId);
                    Console.WriteLine("------------------------");
                    Console.WriteLine(bookReview.ToString());
                    Console.WriteLine("------------------------");
                    Console.WriteLine("(Info: Leaving fields empty will retain their original values)");

                    string? bookTitle;
                    while (true)
                    {
                        Console.Write("Enter new book title: ");
                        bookTitle = Console.ReadLine();
                        if (bookTitle == "0")
                        {
                            Console.WriteLine("Returning to main menu...");
                            State.WaitForUser();
                            return;
                        }else if(string.IsNullOrWhiteSpace(bookTitle))
                        {
                            bookTitle = bookReview.BookTitle;
                        }if (Validation.ValidateBookTitle(bookTitle))
                            break;
                        Console.WriteLine("Invalid book title. Please try again.");
                    }
                    
                    string? genre;
                    while (true)
                    {
                        Console.Write("Enter new genre: ");
                        genre = Console.ReadLine();
                        if (genre == "0")
                        {
                            Console.WriteLine("Returning to main menu...");
                            State.WaitForUser();
                            return;
                        }
                        else if(string.IsNullOrWhiteSpace(genre))
                        {
                            genre = bookReview.Genre;
                        }
                        if (Validation.ValidateGenre(genre))
                            break;
                        Console.WriteLine("Invalid genre. Please try again.");
                    }

                    // Get and validate review text
                    string? reviewText;
                    while (true)
                    {
                        Console.Write("Enter new review: ");
                        reviewText = Console.ReadLine();
                        if (reviewText == "0")
                        {
                            Console.WriteLine("Returning to main menu...");
                            State.WaitForUser();
                            return;
                        }
                        else if(string.IsNullOrWhiteSpace(reviewText))
                        {
                            reviewText = bookReview.Review;
                        }
                        if (Validation.ValidateReviewText(reviewText))
                            break;
                        Console.WriteLine("Invalid review text. Please try again.");
                    }

            BookReview result = _bookReviewService.Update(id, State.currentUser?.UserId, bookTitle, genre, reviewText);
            Console.WriteLine("------------------------");
            Console.WriteLine("Book review updated successfully!"); 
            Console.WriteLine("------------------------");
            Console.WriteLine("Result: ");
            Console.WriteLine(result.ToString());
            State.WaitForUser();

            }else{
                Console.WriteLine("Error: ID is not a number.");
                State.WaitForUser();
                return;
            }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating Book Review: {ex.Message}");
                State.WaitForUser();
            }
        }

     private void DeleteBookReview()
        {
            string? input;

            Console.Clear();
            Console.WriteLine("Review Shelf:");
            Console.WriteLine("------------------------");
            Console.WriteLine("Delete a Book Review:");
            Console.WriteLine("------------------------");
            Console.WriteLine("(or press 0 to return to the main menu):");
            Console.WriteLine("------------------------");
           
            while (true)
                {
                    Console.WriteLine("Enter Review ID:");
                    input = Console.ReadLine();
                    if (input == "0")
                    {
                        Console.WriteLine("Returning to main menu...");
                        State.WaitForUser();
                        return;
                    }
                    if (Validation.ValidateInteger(input))
                        break;
                    Console.WriteLine("Invalid ID. Please try again.");
            }
            
            try
            {
                bool success = int.TryParse(input, out int id);

                if (success)
                {
                    BookReview bookReview = _bookReviewService.GetBookReviewById(id, State.currentUser?.UserId);
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Book Review Found");
                    Console.WriteLine(bookReview.ToString());
                    ConfirmDelete(id);
                }else{
                    Console.WriteLine("Error: ID is not a number.");
                    State.WaitForUser();
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book review: {ex.Message}");
                State.WaitForUser();
            }

        }   

        private void ViewBookReviews()
        {
            Console.Clear();
            Console.WriteLine("Review Shelf:");
            Console.WriteLine("------------------------");
            Console.WriteLine("View Book Reviews:");
            Console.WriteLine("------------------------");

            var reviews = _bookReviewService.GetReviewsByUserId(State.currentUser?.UserId);

            if (reviews == null || reviews.Count == 0)
                {
                    Console.WriteLine("No Reviews Found.");
                    Console.WriteLine();
                }
                else
                {
                    foreach (var review in reviews)
                    {
                        Console.WriteLine(review.ToString());
                        Console.WriteLine("------------------------");
                    }
                }

            State.WaitForUser();
        }

        
    }
}
