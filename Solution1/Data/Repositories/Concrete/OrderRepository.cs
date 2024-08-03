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
    public void GetOrdersDesc()
    {
        foreach (var order in _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }
    public void GetOrdersByCustomer(int id)
    {
        foreach (var order in _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Where(x => x.Customers.Id == id).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }
    public void GetOrdersByDate(DateTime date)
    {
        foreach (var order in _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }
    public int GetOrdersByDateCount(DateTime date)
    {
        return _context.Orders.Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date).Count();
    }
    public void GetOrdersBySeller(int id)
    {
        foreach (var order in _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Where(x => x.Sellers.Id == id).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }
    public int GetOrdersCountByCustomer(int id)
    {
        return _context.Orders.Count(x=> x.CustomerId==id);
    }
    public int GetOrdersCountBySeller(int id)
    {
        return _context.Orders.Count(x => x.SellerId == id);
    }
    public void GetOrder(int id)
    {
        foreach (var order in _context.Orders.Where(x => x.SellerId == id).Include(x => x.Sellers).Include(x => x.Products).Include(x => x.Customers))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }
    public void GetOrdersByDateWithCustomerId(DateTime date, int id)
    {
        foreach (var order in _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date && x.CustomerId == id))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }

    public void GetOrdersByDateWithSellerId(DateTime date, int id)
    {
        foreach (var order in _context.Orders.Include(x => x.Sellers).Include(x => x.Customers).Include(x => x.Products).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).Where(x => x.CreatedDate.Date == date.Date || x.UpdatedDate.Date == date.Date && x.SellerId == id))
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }
}
