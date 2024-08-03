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
    public void GetAllProducts()
    {
        foreach (var product in _context.Products.Include(x => x.Category))
        {
            Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Price: {product.Price} | Count: {product.Count} | Category: {product.Category.Name}");
        }
    }
    public void SearchProduct(string input)
    {
        var products = _context.Products.Include(x => x.Category).Where(x => x.Name.Contains(input)).ToList();

        if (!products.Any())
        {
            ErrorMessages.NotFoundMessage("product");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Price: {product.Price} | Count: {product.Count} | Category: {product.Category.Name}");
            }
        }
    }
    public void GetAllProductsBySellerId(int sellerId)
    {
        foreach (var product in _context.Products.Include(x => x.Category).Include(x => x.Seller).Where(x => x.SellerId == sellerId))
        {
            Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Price: {product.Price} | Count: {product.Count} | Category: {product.Category.Name}");
        }
    }

}
