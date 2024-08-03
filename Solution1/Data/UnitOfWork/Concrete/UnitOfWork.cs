using Core.Messages;
using Data.Repositories.Concrete;
using Data.UnitOfWork.Abstract;

namespace Data.UnitOfWork.Concrete;

public class Unitofwork : IUnitOfWork
{
    public readonly AdminRepository Admins;
    public readonly CustomerRepository Customers;
    public readonly SellerRepository Sellers;
    public readonly CategoryRepository Categories;
    public readonly ProductRepository Products;
    public readonly OrderRepository Orders;
    private readonly ConsoleCommerceAppDbContext _context;
    public Unitofwork()
    {
        _context = new ConsoleCommerceAppDbContext();
        Admins = new AdminRepository(_context);
        Customers = new CustomerRepository(_context);
        Sellers = new SellerRepository(_context);
        Categories = new CategoryRepository(_context);
        Products = new ProductRepository(_context);
        Orders = new OrderRepository(_context);
    }
    public void Commit(string name, string type)
    {
        try
        {
            _context.SaveChanges();
            BasicMessages.SuccessMessage(name, type);
        }
        catch (Exception)
        {
            ErrorMessages.ErrorMessage();
        }
    }
}
