using Domain.Abstractions;
using Domain.Entities;


namespace DataAccess.Repositories
{
    public class ProjectsRepository : Repository<ProjectEntity, Guid>, IProjectsRepository
    {
        public ProjectsRepository(DataContext context) : base(context){ }

    }
}
