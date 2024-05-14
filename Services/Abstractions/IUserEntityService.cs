using Models.Dto;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс сервиса работы с пользователями.
/// </summary>
public interface IUserEntityService
{
    /// <summary>
    /// Получить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>ДТО Пользователя.</returns>
    Task<UserDto> GetByIdAsync(Guid id);
    /// <summary>
    /// Создание пользователя.
    /// </summary>
    /// <param name="creatingUserDto">ДТО создаваемого пользователя.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    Task<Guid> CreateAsync(CreatingUserDto creatingUserDto);
    /// <summary>
    /// Обновление пользователя.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого пользователя.</param>
    /// <param name="updatingUserDto">ДТО обновления пользователя.</param>
    /// <returns></returns>
    Task UpdateAsync(Guid id, UserDto updatingUserDto);
    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}