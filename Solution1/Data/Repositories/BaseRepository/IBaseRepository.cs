using Core;

namespace Data.Repositories.BaseRepository;

public interface IBaseRepository<T> where T : Base
{
    List<T> GetAll();
    T Get(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
