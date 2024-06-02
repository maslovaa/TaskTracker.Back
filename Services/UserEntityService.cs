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
    ///<inheritdoc/>
    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<UserEntity, UserDto>(user);
    }

    ///<inheritdoc/>
    public async Task<Guid> CreateAsync(CreatingUserDto creatingUserDto)
    {
        var user = _mapper.Map<CreatingUserDto, UserEntity>(creatingUserDto);
        return await _userRepository.AddAsync(user);
    }

    ///<inheritdoc/>
    public async Task UpdateAsync(Guid id, UserDto updatingUserDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            throw new ArgumentNullException($"Пользователя с идентификатором {id} не найден!");
        }

        user.UserName = updatingUserDto.UserName;
        user.Email = updatingUserDto.Email;
        await _userRepository.UpdateAsync(user, CancellationToken.None);
      
    }

    ///<inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.DeleteAsync(id, cancellationToken);
    }
}