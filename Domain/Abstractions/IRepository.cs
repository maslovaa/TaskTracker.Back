using System.Linq.Expressions;

namespace Domain.Abstractions;

/// <summary>
/// Интерфейс репозитория.
/// </summary>
/// <typeparam name="T">Тип репозитоиря.</typeparam>
/// <typeparam name="TId">Тип идентификатора в репозитории.</typeparam>
public interface IRepository<T, TId> 
    where TId : struct 
    where T : IEntity<TId>
{
    /// <summary>
    /// Получение всех сущностей.
    /// </summary>
    /// <param name="noTracking">Флаг отслеживания.</param>
    /// <returns>Объект запроса.</returns>
    IQueryable<T> GetAll(bool noTracking = false);

    /// <summary>
    /// Асинхронное получение всех сущностей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="noTracking">Флаг отслеживания.</param>
    /// <returns>Коллекция элементов.</returns>
    Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, bool noTracking = false);

    /// <summary>
    /// Добавление сущностей в хранилище.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Идентификатор сущности.</returns>
    TId Add(T entity);

    /// <summary>
    /// Асинхронное добавление сущности.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Идентификатор сущности.</returns>
    Task<TId> AddAsync(T entity);

    /// <summary>
    /// Получение сущности.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Запрашиваемая сущность.</returns>
    T GetById(TId id);

    /// <summary>
    /// Асинхронное получение сущности.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Запрашиваемая сущность.</returns>
    Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронное получение коллекции сущностей по предикату.
    /// </summary>
    /// <param name="predicate">Предикат.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция элементов.</returns>
    Task<ICollection<T>> GetByPredicate(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление сущности.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат операции обновления.</returns>
    Task<bool> Update(T entity);

    /// <summary>
    /// Удаление сущности.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат операции удаления.</returns>
    Task<bool> Delete(TId id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление сущности.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат операции удаления.</returns>
    bool Delete(T entity);
}