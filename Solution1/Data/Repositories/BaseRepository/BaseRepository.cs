using Core;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.BaseRepository;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly ConsoleCommerceAppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public BaseRepository(ConsoleCommerceAppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public void Add(T entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.UpdatedDate = DateTime.Now;
        _dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public T Get(int id)
    {
        return _dbSet.Find(id);
    }

    public List<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Update(T entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _dbSet.Update(entity);
    }
}
