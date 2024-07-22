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
                Console.WriteLine("4. View Book Reviews");
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
                        //UpdateBookReview();
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

private void CreateBookReview()
{
    Console.Clear();
    Console.WriteLine("Review Shelf:");
    Console.WriteLine("------------------------");
    Console.WriteLine("Create a Book Review:");
    Console.WriteLine("------------------------");
    Console.WriteLine("(or press 1 to return to the main menu):");
    Console.WriteLine("------------------------");

    // Get and validate book title
    string? bookTitle;
    while (true)
    {
        Console.Write("Enter book title: ");
        bookTitle = Console.ReadLine();
        if (bookTitle == "1")
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
        if (genre == "1")
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
        if (reviewText == "1")
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
       
    // private void UpdateBookReview()
    // {
    // Console.Clear();
    // Console.WriteLine("Update Book Review:");
    // Console.WriteLine("------------------------");

    // // Get review ID from user
    // Console.Write("Enter Review ID: ");
    // if (!int.TryParse(Console.ReadLine(), out int reviewId))
    // {
    //     Console.WriteLine("Invalid Review ID.");
    //     return;
    // }

    // // Get updated details from user
    // Console.Write("Enter new Book Title: ");
    // var bookTitle = Console.ReadLine();

    // Console.Write("Enter new Genre: ");
    // var genre = Console.ReadLine();

    // Console.Write("Enter new Review Text: ");
    // var reviewText = Console.ReadLine();

    // // Validate input
    // if (string.IsNullOrEmpty(bookTitle) || string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(reviewText))
    // {
    //     Console.WriteLine("All fields must be filled.");
    //     return;
    // }

    // // Create the updated BookReview object
    // var updatedReview = new BookReview
    // {
    //     BookReviewId = reviewId,
    //     BookTitle = bookTitle,
    //     Genre = genre,
    //     Review = reviewText,
    //     UserId = State.currentUser?.UserId ?? 0 // Assuming you want to keep the same user ID
    // };

    // try
    // {
    //     // Call the service method to update the review
    //     _bookReviewService.Update(updatedReview);
    //     Console.WriteLine("Book review updated successfully.");
    // }
    // catch (KeyNotFoundException ex)
    // {
    //     Console.WriteLine(ex.Message);
    // }
    // catch (Exception ex)
    // {
    //     Console.WriteLine($"An error occurred: {ex.Message}");
    // }

    // State.WaitForUser();
    // }


        private void DeleteBookReview()
        {
            // Console.Clear();
            // Console.WriteLine("Delete a Book Review:");
            // Console.WriteLine("------------------------");

            // Console.Write("Enter the ID of the review to delete: ");
            // if (int.TryParse(Console.ReadLine(), out int reviewId))
            // {
            //     try
            //     {
            //         _bookReviewService.DeleteReview(reviewId);
            //         Console.WriteLine("Book review deleted successfully!");
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine($"Error deleting book review: {ex.Message}");
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("Invalid review ID.");
            // }

            // WaitForUser();
        }

        private void ViewBookReviews()
        {
            Console.Clear();
            Console.WriteLine("Review Shelf:");
            Console.WriteLine("------------------------");
            Console.WriteLine("View Book Reviews:");
            Console.WriteLine("------------------------");

            var reviews = _bookReviewService.GetReviewsByUserId(State.currentUser?.UserId);

            if (reviews == null || !reviews.Any())
                {
                    Console.WriteLine("No Reviews Have Been Made.");
                    Console.WriteLine();
                }
                else
                {
                    foreach (var review in reviews)
                    {
                        Console.WriteLine($"ID: {review.BookReviewId}, Title: {review.BookTitle}, Review: {review.Review}");
                    }
                }

            State.WaitForUser();
        }

        
    }
}
