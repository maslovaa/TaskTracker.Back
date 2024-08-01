using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DesksRepository : Repository<DeskEntity, Guid>, IDesksRepository
    {
        public DesksRepository(DataContext context) : base(context){ }

        public async Task<DeskEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.DeskEntities.Include(x => x.Tasks).ThenInclude(x => x.Status).FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
            return entity;
        }
    }
}
