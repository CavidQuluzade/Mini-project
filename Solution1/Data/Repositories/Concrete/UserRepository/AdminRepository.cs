using Core.Entities;
using Data.Repositories.Abstract;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Concrete;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    private readonly ConsoleCommerceAppDbContext _context;
    public AdminRepository(ConsoleCommerceAppDbContext context) : base(context)
    {
        _context = context;
    }
    public bool ExistAdminEmail(string email)
    {
        var existAdminEmail = _context.Admins.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        if (existAdminEmail != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
