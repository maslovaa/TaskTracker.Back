using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProjectsRepository(DataContext context) : Repository<ProjectEntity, Guid>(context), IProjectsRepository
    {
        public IQueryable GetAllWithRelated()
        {
            return context.ProjectEntities
                .Where(p => p.IsActive)
                .Include(x=>x.Desks).ThenInclude(x=>x.Tasks)
                .Include(x => x.Owner).ThenInclude(x => x.Role)
                .Include(x => x.Users).ThenInclude(x => x.Role);
        }

        public async Task<ProjectEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.ProjectEntities.Include(x => x.Desks).FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);
            return entity;
        }
    }
}
