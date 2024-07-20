using Microsoft.EntityFrameworkCore;
using ReviewShelf.Entities;

namespace ReviewShelf.DAO
{
    public class LoginDAO : IDAO<Login>
    {
        private readonly ApplicationDbContext _context;

          public LoginDAO(ApplicationDbContext context)
            {
                _context = context;
            }

        // Add a new Login to the database
        public void Create(Login item)
        {
            _context.Logins.Add(item);  
            _context.SaveChanges();     
        }

        // Delete a Login from the database
        public void Delete(Login item)
        {
            _context.Logins.Remove(item);  
            _context.SaveChanges();       
        }

        // Retrieve all Logins from the database
        public ICollection<Login> GetAll()
        {
            // Retrieves all Login entities from the database, including related User entities
            List<Login> logins = _context.Logins.Include(l => l.User).ToList();
            return logins;
        }

        // Retrieve a Login by its ID
        public Login GetById(int ID)
        {
            // Retrieves a Login entity with the specified ID from the database, including the related User entity
            Login? login = _context.Logins.Include(l => l.User).FirstOrDefault(l => l.LoginId == ID);

            if (login == null)
                throw new KeyNotFoundException($"Login with ID: {ID} not found.");

            return login;
        }

        // Update an existing Login
        public void Update(Login newItem)
        {

            // Retrieves the existing Login entity to be updated
            Login? originalLogin = _context.Logins.FirstOrDefault(l => l.LoginId == newItem.LoginId);

            if (originalLogin != null)
            {
                // Updates the properties of the original Login entity
                originalLogin.Username = newItem.Username;

                //TODO Ensure password is securely handled!!
                originalLogin.Password = newItem.Password; 


                _context.Logins.Update(originalLogin);  
                _context.SaveChanges();                 
            }
            else
            {
                throw new KeyNotFoundException($"Login with ID {newItem.LoginId} not found.");
            }
        }

        // Retrieve a Login by username and password
        public Login GetLoginByUsernameAndPassword(string username, string password)
        {
            // Retrieves a Login entity with the specified username and password, including the related User entity
            Login? login = _context.Logins.Include(l => l.User)
                                        .FirstOrDefault(l => l.Username == username && l.Password == password);

            if(login == null)
            {
                throw new KeyNotFoundException($"Login with {username} not found.");
            }

            return login;
        }
    }
}
