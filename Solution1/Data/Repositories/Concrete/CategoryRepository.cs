using Core.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Concrete;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly ConsoleCommerceAppDbContext _context;
    public CategoryRepository(ConsoleCommerceAppDbContext context) : base(context)
    {
        _context = context;
    }
    public void GetAllCategories()
    {
        foreach (var category in _context.Categories)
        {
            Console.WriteLine($"Id: {category.Id} | Name: {category.Name}");
        }
    }

    public Category GetCategoryByName(string name)
    {
        return _context.Categories.FirstOrDefault(c => c.Name == name);
    }

    public int GetCategoryCount()
    {
        return _context.Categories.Count();
    }
}
