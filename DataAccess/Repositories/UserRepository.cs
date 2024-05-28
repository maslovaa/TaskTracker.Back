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

}