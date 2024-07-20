using ReviewShelf.DAO;
using ReviewShelf.Entities;
using ReviewShelf.Service;

public class LoginService: IService<Login>{

    private readonly LoginDAO _loginDao;

    public LoginService(LoginDAO loginDao)
    {
         _loginDao = loginDao;
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