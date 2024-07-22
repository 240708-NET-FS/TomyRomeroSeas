using ReviewShelf.Utility.Exceptions;
using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Service;

public class LoginService: IService<Login>{

    private readonly LoginDAO _loginDao;

    public LoginService(LoginDAO loginDao)
    {
         _loginDao = loginDao;
    }

    public bool Authenticate(string? username, string? password)
    {
        try
        {
            if(username == null || password == null)
            {
                throw new LoginException("(Warning): Username or Password is empty");
            }

            var login = _loginDao.GetLoginByUsernameAndPassword(username, password);
            // If no exception is thrown, credentials are valid
            return login != null;
        }
        catch (KeyNotFoundException)
        {
            // Credentials are invalid
            return false;
        }
    }

    public void Create(Login item){
        throw new NotImplementedException();
    }

    public void Delete(Login item)
    {
        throw new NotImplementedException();
    }

    public void Update(Login item)
    {
        throw new NotImplementedException();
    }

    public ICollection<Login> GetAll(){
        throw new NotImplementedException();
    }

}