using Domain.Entities;

namespace Domain.Abstractions;

/// <summary>
/// Интерфейс хранилища пользоватлей.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получение всех пользоватлей.
    /// </summary>
    /// <param name="NoTracking">Флаг отслеживания.</param>
    /// <returns>Объект запроса.</returns>
    IQueryable<UserEntity> GetAll(bool NoTracking = false);

    /// <summary>
    /// Асинхронное получение всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="asNoTracking">Флаг отслеживания</param>
    /// <returns>Коллекция пользоватлей.</returns>
    Task<ICollection<UserEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Пользователь.</returns>
    UserEntity Get(Guid id);

    /// <summary>
    /// Асинхронное получение пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    Task<UserEntity> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Результат операции.</returns>
    bool Delete(Guid id);

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="user">Объект пользователя.</param>
    /// <returns>Результат операции.</returns>
    bool Delete(UserEntity user);

    /// <summary>
    /// Обновление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    void Update(UserEntity user);

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Добавленный пользователь.</returns>
    UserEntity Add(UserEntity user);

    /// <summary>
    /// Асинхронное добавление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Добавленный пользователь.</returns>
    Task<UserEntity> AddAsync(UserEntity user);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    void SaveChanges();

    /// <summary>
    /// Асинхронное сохранение изменений.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken cancellationToken);
}