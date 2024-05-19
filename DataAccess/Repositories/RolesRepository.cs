using Domain.Abstractions;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class RolesRepository(DataContext _context) : Repository<RoleEntity, Guid>(_context), IRolesRepository
    {
    }
}
