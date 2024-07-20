namespace ReviewShelf.Service;

public interface IService<T>{

    //CRUD Operations for Service Layer

    public void Create(T item)
    {

    }

    public void Update(T item)
    {

    }

    public void Delete(T item)
    {

    }

    public ICollection<T> GetAll();

}

