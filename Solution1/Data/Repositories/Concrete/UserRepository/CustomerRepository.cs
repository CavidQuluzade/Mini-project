using Core.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Concrete;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly ConsoleCommerceAppDbContext _context;
    public CustomerRepository(ConsoleCommerceAppDbContext context) : base(context)
    {
        _context = context;
    }
    public Customer GetCustomerById(int id)
    {
        return _context.Customers.FirstOrDefault(x => x.Id == id);

    }

    public int GetCustomersCount()
    {
        return _context.Customers.Count();
    }
    public bool ExistCustomerEmail(string email)
    {
        var existCustomerEmail = _context.Customers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        if (existCustomerEmail != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ExistCustomerPIN(string pin)
    {
        var existCustomerPIN = _context.Customers.FirstOrDefault(x => x.Pin.ToLower() == pin.ToLower());
        if (existCustomerPIN != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ExistCustomerSeria(int seria)
    {
        var existCustomerSeria = _context.Customers.FirstOrDefault(x => x.SeriaNumber == seria);
        if (existCustomerSeria != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
