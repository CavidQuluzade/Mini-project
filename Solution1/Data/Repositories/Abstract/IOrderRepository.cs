using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface IOrderRepository : IBaseRepository<Order>
{
    List<Order> GetOrdersDesc();
    List<Order> GetOrdersByCustomer(int id);
    List<Order> GetOrdersByDate(DateTime date);
    List<Order> GetOrdersBySeller(int id);
    List<Order> GetOrdersByDateWithCustomerId(DateTime date, int id);
    List<Order> GetOrdersByDateWithSellerId(DateTime date, int id);
    int GetOrdersCountByCustomer(int id);
    int GetOrdersCountBySeller(int id);
}
