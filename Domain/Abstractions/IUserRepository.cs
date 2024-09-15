using Domain.Entities;

namespace Domain.Abstractions;

/// <summary>
/// Интерфейс хранилища пользоватлей.
/// </summary>
public interface IUserRepository : IRepository<UserEntity, Guid>
{
}