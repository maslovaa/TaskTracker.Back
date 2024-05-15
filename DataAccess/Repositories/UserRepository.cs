using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

/// <summary>
/// Хранилище пользователей.
/// </summary>
public class UserRepository(DataContext _context) : IUserRepository
{
    private readonly DbSet<UserEntity> _usersSet = _context.Set<UserEntity>();

    /// <summary>
    /// Получение запроса на всех пользователей.
    /// </summary>
    /// <param name="NoTracking">Флаг отслеживания изменений.</param>
    /// <returns>Объект запроса.</returns>
    public IQueryable<UserEntity> GetAll(bool NoTracking = false)
    {
        _context.ProjectEntities.Add(new ProjectEntity() { Name = "sdff" });
        return NoTracking ? _usersSet.AsNoTracking() : _usersSet;
    }

    /// <summary>
    /// Асинхронное получение всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <param name="asNoTracking">Флаг отслеживания изменений.</param>
    /// <returns>Коллекция пользователей.</returns>
    public async Task<ICollection<UserEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        return await GetAll().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Пользователь.</returns>
    public UserEntity Get(Guid id)
    {
        return _usersSet.Find(id);
    }

    /// <summary>
    /// Асинхронное получение пользователей.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция пользователей.</returns>
    public async Task<UserEntity> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _usersSet.FindAsync(id);
    }

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Флаг результата процедуры.</returns>
    public bool Delete(Guid id)
    {
        var user = _usersSet.Find(id);
        if (user is null)
        {
            return false;
        }

        _usersSet.Remove(user);
        return true;
       
    }

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Флаг результата процедуры.</returns>
    public bool Delete(UserEntity user)
    {
        if (user is null)
        {
            return false;
        }

        _usersSet.Entry(user).State = EntityState.Deleted;
        return true;
    }

    /// <summary>
    /// Обновление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    public void Update(UserEntity user)
    {
        _usersSet.Entry(user).State = EntityState.Modified;
    }

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Добавленный пользователь.</returns>
    public UserEntity Add(UserEntity user)
    {
        return _usersSet.Add(user).Entity;
    }

    /// <summary>
    /// Асинхронное добавление пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Пользователь.</returns>
    public async Task<UserEntity> AddAsync(UserEntity user)
    {
       return (await _usersSet.AddAsync(user)).Entity;
    }

    /// <summary>
    /// Сохранение изменений.
    /// </summary>
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}