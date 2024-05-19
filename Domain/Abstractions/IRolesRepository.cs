using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IRolesRepository : IRepository<RoleEntity, Guid>
    {
    }
}
