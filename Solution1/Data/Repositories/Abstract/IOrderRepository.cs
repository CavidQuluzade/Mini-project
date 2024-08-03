using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface IOrderRepository : IBaseRepository<Order>
{
    void GetOrdersDesc();
    void GetOrdersByCustomer(int id);
    void GetOrdersByDate(DateTime date);
    void GetOrdersBySeller(int id);
    void GetOrder(int id);
    void GetOrdersByDateWithCustomerId(DateTime date, int id);
    void GetOrdersByDateWithSellerId(DateTime date, int id);
    int GetOrdersCountByCustomer(int id);
    int GetOrdersCountBySeller(int id);
}
