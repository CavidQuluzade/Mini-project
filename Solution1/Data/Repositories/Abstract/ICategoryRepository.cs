using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface ICategoryRepository : IBaseRepository<Category>
{
    void GetAllCategories();
    int GetCategoryCount();
    Category GetCategoryByName(string name);
}
