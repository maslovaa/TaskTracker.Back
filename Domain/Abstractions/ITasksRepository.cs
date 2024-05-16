using Domain.Entities;
using Models.Dto;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface ITasksRepository
    {
        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <returns></returns>
        Task<TaskDto> FindTaskByIdAsync(Guid taskId);

        /// <summary>
        /// Получение коллекции задач по переданному выражению
        /// </summary>
        /// <param name="predicate">Выражение филтра для отбора сущностей</param>
        /// <returns></returns>
        Task<List<TaskDto>> GetTasksByPredicateAsync(Expression<Func<TaskEntity, bool>> predicate);

        /// <summary>
        /// Добавление задачи
        /// </summary>
        /// <param name="taskDto">Входной объект задачи</param>
        /// <returns></returns>
        Task<Guid> AddTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="taskDto">Входной объект задачи</param>
        /// <returns></returns>
        Task<bool> UpdateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <returns></returns>
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
