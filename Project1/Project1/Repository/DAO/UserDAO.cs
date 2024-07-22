using ReviewShelf.Entities;
using ReviewShelf.Utility.Exceptions;

namespace ReviewShelf.DAO
{
    public class UserDAO : IDAO<User>
    {
        private readonly ApplicationDbContext _context;

        public UserDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        //Add a new User to the database
        public void Create(User item)
        {
            // Check if a user with the same username already exists
            bool usernameExists = _context.Users.Any(u => u.UserName == item.UserName);
            
            if (usernameExists)
            {
                throw new UserCreationException("A user with this username already exists.");
            }

            _context.Users.Add(item);  
            _context.SaveChanges();    
        }

        //Delete a User from the database
        public void Delete(User item)
        {
            _context.Users.Remove(item);  
            _context.SaveChanges();    
        }

        // Retrieve all Users from the database
        public ICollection<User> GetAll()
        {
            // Retrieves all User entities from the database and returns as a list
            List<User> users = _context.Users.ToList();
            return users;
        }

        // Retrieve a User by their ID
        public User GetById(int ID)
        {
            // Retrieves a User entity with the specified ID from the database
            var user = _context.Users.FirstOrDefault(u => u.UserId == ID);

            // Throws an exception if the user is not found
            if (user == null)
                throw new KeyNotFoundException($"User with ID {ID} not found.");

            return user;
        }

        // Update an existing User
        public void Update(User newItem)
        {

            // Retrieves the existing User entity to be updated
            var originalUser = _context.Users.FirstOrDefault(u => u.UserId == newItem.UserId);

            if (originalUser != null)
            {
                // Updates the properties of the original User entity
                originalUser.FirstName = newItem.FirstName;
                originalUser.LastName = newItem.LastName;

                _context.Users.Update(originalUser);  
                _context.SaveChanges();               
            }
            else
            {
                // Throws an exception if the original User entity is not found
                throw new KeyNotFoundException($"User with ID {newItem.UserId} not found.");
            }
        }

        // Retrieve a User by their username
        public User GetByUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
                throw new KeyNotFoundException($"User with username {username} not found.");

            return user;
        }
    }
}
