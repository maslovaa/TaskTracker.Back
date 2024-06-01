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
                .Include(x=>x.Desks).ThenInclude(x=>x.Tasks)
                .Include(x => x.Owner).ThenInclude(x => x.Role)
                .Include(x => x.Users).ThenInclude(x => x.Role);
        }
    }
}
