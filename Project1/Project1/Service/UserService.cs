using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Service;
using ReviewShelf.Utility;
using ReviewShelf.Utility.Exceptions;

public class UserService: IService<User>{

    private readonly UserDAO _userDao;

    public UserService(UserDAO userDao)
    {
         _userDao = userDao;
    }

    public void SetUserState(string? username)
        {
            try
            {
                if(username == null)
                {
                    throw new UserNotFoundException($"Username is empty.");
                }

                // Use the DAO to get the user by username
                State.currentUser = _userDao.GetByUsername(username);
            }
            catch (KeyNotFoundException ex)
            {
                // Handle the exception as needed (e.g., log it or rethrow it)
                throw new UserNotFoundException($"User with username {username} not found.", ex);
            }
        }

    public void Create(User item){
        throw new NotImplementedException();
    }

    public void Delete(User item)
    {
        throw new NotImplementedException();
    }

    public void Update(User item)
    {
        throw new NotImplementedException();
    }

    public ICollection<User> GetAll(){
        throw new NotImplementedException();
    }



}