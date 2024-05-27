using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

/// <summary>
/// Хранилище пользователей.
/// </summary>
public class UserRepository : Repository<UserEntity, Guid>, IUserRepository
{
    public UserRepository(DataContext context) : base(context){ }

    /// <summary>
    /// Переопределенный метод мягкого удаления для пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат операции удаления.</returns>
    public override async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            return false;
        }

        user.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Переопределенный метод мягкого удаления для пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Результат операции удаления.</returns>
    public override bool Delete(UserEntity user)
    {
        if (user is null || !user.IsActive)
        {
            return false;
        }

        user.IsActive = false;
        _context.Entry(user).State = EntityState.Deleted;
        _context.SaveChanges();
        return true;
    }
}