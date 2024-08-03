using Core.Entities;
using Data.Repositories.BaseRepository;

namespace Data.Repositories.Abstract;

internal interface IAdminRepository : IBaseRepository<Admin>
{
    bool ExistAdminEmail(string email);
}
