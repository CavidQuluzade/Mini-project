using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface ICategoryRepository : IBaseRepository<Category>
{
    int GetCategoryCount();
    Category GetCategoryByName(string name);
}
