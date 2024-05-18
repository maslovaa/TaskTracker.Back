using Domain.Abstractions;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class TasksRepository : Repository<TaskEntity, Guid>, ITasksRepository
    {
        public TasksRepository(DataContext context) : base(context){ }
    }
}
