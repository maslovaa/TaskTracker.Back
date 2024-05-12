using Domain.Entities;
using Models.Dto;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface IProjectsRepository
    {
        /// <summary>
        /// Получение проекта по идентификатору
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns></returns>
        Task<ProjectDto> FindProjectByIdAsync(Guid projectId);

        /// <summary>
        /// Получение коллекции проектов по переданному выражению
        /// </summary>
        /// <param name="predicate">Выражение филтра для отбора сущностей</param>
        /// <returns></returns>
        Task<List<ProjectDto>> GetProjectsByPredicateAsync(Expression<Func<ProjectEntity, bool>> predicate);

        /// <summary>
        /// Добавление проекта
        /// </summary>
        /// <param name="projectDto">Входной объект проекта</param>
        /// <returns></returns>
        Task<Guid> AddProjectAsync(ProjectDto projectDto);

        /// <summary>
        /// Обновление проекта
        /// </summary>
        /// <param name="projectDto">Входной объект проекта</param>
        /// <returns></returns>
        Task<bool> UpdateProjectAsync(ProjectDto projectDto);

        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns></returns>
        Task<bool> DeleteProjectAsync(Guid projectId);
    }
}