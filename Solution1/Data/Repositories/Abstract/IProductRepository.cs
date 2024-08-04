using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface IProductRepository : IBaseRepository<Product>
{
    Product GetProduct(int id);
    List<Product> GetAllProducts();
    List<Product> SearchProduct(string input);
    List<Product> GetAllProductsBySellerId(int sellerId);
}
