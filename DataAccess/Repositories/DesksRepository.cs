using Domain.Abstractions;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class DesksRepository : Repository<DeskEntity, Guid>, IDesksRepository
    {
        public DesksRepository(DataContext context) : base(context){ }
    }
}
