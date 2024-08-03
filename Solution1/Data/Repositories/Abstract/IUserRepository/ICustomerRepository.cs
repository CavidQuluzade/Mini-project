using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Customer GetCustomerById(int id);
    void GetAllCustomers();
    int GetCustomersCount();
    bool ExistCustomerEmail(string email);
    bool ExistCustomerPIN(string pin);
    bool ExistCustomerSeria(int seria);


}
