using Core.Entities;
using Core.Messages;
using Data.Repositories.Abstract;
using Data.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly ConsoleCommerceAppDbContext _context;
    public ProductRepository(ConsoleCommerceAppDbContext context) : base(context)
    {
        _context = context;
    }
    public Product GetProduct(int id)
    {
        return _context.Products.FirstOrDefault(x => x.Id == id);
    }
    public List<Product> GetAllProducts()
    {
        return _context.Products.Include(x => x.Category).ToList();
        
    }
    public List<Product> SearchProduct(string input)
    {
        var products = _context.Products.Include(x => x.Category).Where(x => x.Name.Contains(input)).ToList();

        return products;
    }
    public List<Product> GetAllProductsBySellerId(int sellerId)
    {
        return _context.Products.Include(x => x.Category).Include(x => x.Seller).Where(x => x.SellerId == sellerId).ToList();
        
    }

}
