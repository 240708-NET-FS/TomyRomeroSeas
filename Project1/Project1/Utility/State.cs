namespace ReviewShelf.Utility;

using ReviewShelf.Entities;

public static class State{

    public static bool isActiveStartUp {get;set;}

    public static bool isActiveMainMenu {get; set;}

    public static User? currentUser{get;set;}

     public static void WaitForUser()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

}