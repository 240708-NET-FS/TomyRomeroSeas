using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Utility.Exceptions;


public class AccountService
{
    private readonly UserDAO _userDao;
    private readonly LoginDAO _loginDao;

    public AccountService(UserDAO userDao, LoginDAO loginDao)
    {
        _userDao = userDao;
        _loginDao = loginDao;
    }

    public void CreateUserWithLogin(string? firstName, string? lastName, string? username, string? password)
    {
         if (string.IsNullOrEmpty(firstName))
        throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty.");
    
        if (string.IsNullOrEmpty(lastName))
            throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty.");
        
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");
        
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");



        using (var transaction = new TransactionScope())
        {
            try
            {
                 
                // Create the User entity
                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = username
                };
                
                // Save the User
                _userDao.Create(user);

                // Create the Login entity and associate it with the User
                var login = new Login
                {
                    Username = username,
                    //Password should be hashed
                    Password = password, 
                    UserId = user.UserId
                };

                // Save the Login
                _loginDao.Create(login);

                // Complete the transaction
                transaction.Complete();
            }
            catch(UserCreationException ex)
            {
                throw new UserCreationException($"A user with this username already exists.", ex);
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, etc.)
                throw new UserCreationException($"An error occurred while creating the user and login.", ex);
            }
        }
    }
}
