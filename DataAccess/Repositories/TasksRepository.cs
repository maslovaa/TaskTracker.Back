using System.Linq.Expressions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TasksRepository : Repository<TaskEntity, Guid>, ITasksRepository
    {
        public TasksRepository(DataContext context) : base(context){ }

        public override async Task<IEnumerable<TaskEntity>> GetByPredicateAsync(Expression<Func<TaskEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<TaskEntity>().Where(predicate).Include(x => x.Performer).ToListAsync(cancellationToken);
        }

        public override async Task<TaskEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.TaskEntities
                .Where(x => x.Id == id && x.IsActive)
                .Include(x => x.Performer)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
