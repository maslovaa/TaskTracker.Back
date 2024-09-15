using Models.Dto;

namespace Services.Abstractions
{

    /// <summary>
    /// Интерфейс сервиса работы с ролями.
    /// </summary>
    public interface IRoleEntityService
    {
        /// <summary>
        /// Получить роль.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>ДТО Ролей.</returns>
        Task<RoleDto> GetByIdAsync(Guid id);
        /// <summary>
        /// Создание роли.
        /// </summary>
        /// <param name="creatingRoleDto">ДТО создаваемой роли.</param>
        /// <returns>Идентификатор созданной роли.</returns>
        Task<Guid> CreateAsync(RoleDto creatingRoleDto);
        /// <summary>
        /// Обновление роли.
        /// </summary>
        /// <param name="id">Идентификатор обновляемой роли.</param>
        /// <param name="updatingRoleDto">ДТО обновления роли.</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, RoleDto updatingRoleDto);
        /// <summary>
        /// Удаление роли.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
