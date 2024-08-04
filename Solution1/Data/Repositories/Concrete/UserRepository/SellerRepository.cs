using Core.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Concrete;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly ConsoleCommerceAppDbContext _context;
    public SellerRepository(ConsoleCommerceAppDbContext context) : base(context)
    {
        _context = context;
    }

    public decimal GetIncome(int id)
    {
        decimal total = 0;
        foreach (var order in _context.Orders.Where(x => x.SellerId == id))
        {
            total += order.TotalAmount;
        }
        return total;
    }

    public Seller GetSellerByEmail(string email, string password)
    {
        return _context.Sellers.FirstOrDefault(x => x.Email == email && x.Password == password);
    }

    public Seller GetSellerById(int id)
    {
        return _context.Sellers.FirstOrDefault(x => x.Id == id);
    }

    public int GetSellersCount()
    {
        return _context.Sellers.Count();
    }
    public bool ExistSellerEmail(string email)
    {
        var existSellerEmail = _context.Sellers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        if (existSellerEmail != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ExistSellerPIN(string pin)
    {
        var existSellerPIN = _context.Sellers.FirstOrDefault(x => x.Pin.ToLower() == pin.ToLower());
        if (existSellerPIN != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ExistSellerSeria(int seria)
    {
        var existSellerSeria = _context.Sellers.FirstOrDefault(x => x.SeriaNumber == seria);
        if (existSellerSeria != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
