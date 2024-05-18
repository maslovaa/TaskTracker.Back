using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Models.Dto;
using Services.Abstractions;

namespace Services;

/// <summary>
/// Сервис работы с пользователем.
/// </summary>
public class UserEntityService : IUserEntityService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public UserEntityService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Дто пользователя.</returns>
    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id, CancellationToken.None);
        return _mapper.Map<UserEntity, UserDto>(user);
    }

    /// <summary>
    /// Создание пользователя.
    /// </summary>
    /// <param name="creatingUserDto">Дто создаваемого пользователя.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    public async Task<Guid> CreateAsync(CreatingUserDto creatingUserDto)
    {
        var user = _mapper.Map<CreatingUserDto, UserEntity>(creatingUserDto);
        return await _userRepository.AddAsync(user);
    }

    /// <summary>
    /// Обновление пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="updatingUserDto">Дто обновления для пользователя.</param>
    /// <exception cref="ArgumentNullException">Исключение если изменяемый пользователь не существует.</exception>
    public async Task UpdateAsync(Guid id, UserDto updatingUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id, CancellationToken.None);
        if (user is null)
        {
            throw new ArgumentNullException($"Пользователя с идентификатором {id} не найден!");
        }

        user.UserName = updatingUserDto.UserName;
        user.Email = updatingUserDto.Email;
        await _userRepository.Update(user);
      
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.Delete(id, CancellationToken.None);
    }
}