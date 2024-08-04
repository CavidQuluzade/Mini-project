using Core.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly ConsoleCommerceAppDbContext _context;
    public OrderRepository(ConsoleCommerceAppDbContext context) : base(context)
    {
        _context = context;
    }
    public List<Order> GetOrdersDesc()
    {
        return _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
    }
    public List<Order> GetOrdersByCustomer(int id)
    {
        return _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Where(x => x.Customers.Id == id).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
        
    }
    public List<Order> GetOrdersByDate(DateTime date)
    {
        return _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date).ToList();        
    }
    public int GetOrdersByDateCount(DateTime date)
    {
        return _context.Orders.Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date).Count();
    }
    public List<Order> GetOrdersBySeller(int id)
    {
        return _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Where(x => x.Sellers.Id == id).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
        
       
    }
    public int GetOrdersCountByCustomer(int id)
    {
        return _context.Orders.Count(x=> x.CustomerId==id);
    }
    public int GetOrdersCountBySeller(int id)
    {
        return _context.Orders.Count(x => x.SellerId == id);
    }

    public List<Order> GetOrdersByDateWithCustomerId(DateTime date, int id)
    {
        return _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date && x.CustomerId == id).ToList();

        
    }

    public List<Order> GetOrdersByDateWithSellerId(DateTime date, int id)
    {
        return _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date && x.SellerId == id).ToList();
        
    }
}
