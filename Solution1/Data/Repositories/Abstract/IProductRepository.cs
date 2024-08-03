using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface IProductRepository : IBaseRepository<Product>
{
    Product GetProduct(int id);
    void GetAllProducts();
    void SearchProduct(string input);
    void GetAllProductsBySellerId(int sellerId);
}
