using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class TasksRepository(DataContext _context, IMapper _mapper) : ITasksRepository
    {
        public async Task<Guid> AddTaskAsync(TaskDto taskDto)
        {
            try
            {
                TaskEntity taskEntity = _mapper.Map<TaskEntity>(taskDto);
                var res = await _context.TaskEntities.AddAsync(taskEntity);
                await _context.SaveChangesAsync();
                return res.Entity.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteTaskAsync(Guid taskId)
        {
            try
            {
                var task = await _context.TaskEntities.FindAsync(taskId);
                _context.TaskEntities.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TaskDto> FindTaskByIdAsync(Guid taskId)
        {
            try
            {
                TaskEntity taskEntity = await _context.TaskEntities.FindAsync(taskId);
                TaskDto taskDto = _mapper.Map<TaskDto>(taskEntity);
                return taskDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<TaskDto>> GetTasksByPredicateAsync(Expression<Func<TaskEntity, bool>> predicate)
        {
            try
            {
                List<TaskEntity> taskEntity = await _context.TaskEntities.Where(predicate).ToListAsync();
                List<TaskDto> taskDto = _mapper.Map<List<TaskDto>>(taskEntity);
                return taskDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateTaskAsync(TaskDto taskDto)
        {
            try
            {
                TaskEntity taskEntity = _mapper.Map<TaskEntity>(taskDto);
                _context.TaskEntities.Update(taskEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
