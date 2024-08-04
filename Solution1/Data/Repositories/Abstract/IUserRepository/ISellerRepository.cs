using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface ISellerRepository : IBaseRepository<Seller>
{
    decimal GetIncome(int id);
    Seller GetSellerByEmail(string email, string password);
    Seller GetSellerById(int id);
    int GetSellersCount();
    bool ExistSellerEmail(string email);
    bool ExistSellerPIN(string pin);
    bool ExistSellerSeria(int seria);

}
