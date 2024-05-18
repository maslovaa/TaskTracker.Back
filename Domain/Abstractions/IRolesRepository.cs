using Domain.Entities;
using Models.Dto;
using System.Linq.Expressions;

namespace Domain.Abstractions
{
    public interface IRolesRepository
    {
        /// <summary>
        /// Получение роли по идентификатору
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <returns></returns>
        Task<RoleDto> FindRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Получение коллекции ролей по переданному выражению
        /// </summary>
        /// <param name="predicate">Выражение фильтра для отбора сущностей</param>
        /// <returns></returns>
        Task<List<RoleDto>> GetRolesByPredicateAsync(Expression<Func<RoleEntity, bool>> predicate);

        /// <summary>
        /// Добавление роли
        /// </summary>
        /// <param name="roleDto">Входной объект роли</param>
        /// <returns></returns>
        Task<Guid> AddRoleAsync(RoleDto roleDto);

        /// <summary>
        /// Обновление роли
        /// </summary>
        /// <param name="roleDto">Входной объект роли</param>
        /// <returns></returns>
        Task<bool> UpdateRoleAsync(RoleDto roleDto);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <returns></returns>
        Task<bool> DeleteRoleAsync(Guid roleId);


    }
}
