using AutoMapper;
using Domain.Entities;
using Models.Dto;
using Services.AutoMapper;
using Tests.Builders;

namespace Tests.Services.AutoMapper;

public class UserMapperTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Test]
    public void Map_WhenGivenValidSourceObjectUserDtoOrUserEntity_ReturnsValidOriginObjectUserDtoOrUserEntity()
    {
        // Arrange
        
        var userDto = new UserDto()
        {
            UserName = "SomeUserName",
            Email = "someMailBox@domain.ru"
        };

        var userEntity = UserBuilder.CreateBaseUser();
        
        // Act
        var userResult = _mapper.Map<UserEntity>(userDto);
        var userDtoResult = _mapper.Map<UserDto>(userEntity);
        
        // Assert
        Assert.AreEqual(userDto.UserName, userResult.UserName);
        Assert.AreEqual(userDto.Email, userResult.Email);
        Assert.AreEqual(userEntity.UserName, userDtoResult.UserName);
        Assert.AreEqual(userEntity.Email, userDtoResult.Email);
        
    }
    
    [Test]
    public void Map_WhenGivenValidSourceObjectCreatingUserDtoOrUserEntity_ReturnsValidOriginObjectCreatingUserDtoOrUserEntity()
    {
        // Arrange

        var creatingUserDto = new CreatingUserDto()
        {
            Name = "SomeNewUserName",
            Surname = "SomeNewUserSurname",
            Patronymic = "SomeNewUserPatronymic",
            UserName = "SomeNewUserNickname",
            Email = "SomeNewUserEmail"
        };

        var userEntity = UserBuilder.CreateBaseUser();
        
        // Act
        var userResult = _mapper.Map<UserEntity>(creatingUserDto);
        var userDtoResult = _mapper.Map<CreatingUserDto>(userEntity);
        
        // Assert
        Assert.AreEqual(creatingUserDto.Name, userResult.Name);
        Assert.AreEqual(creatingUserDto.Surname, userResult.Surname);
        Assert.AreEqual(creatingUserDto?.Patronymic, userResult?.Patronymic);
        Assert.AreEqual(creatingUserDto.UserName, userResult.UserName);
        Assert.AreEqual(creatingUserDto.Email, userResult.Email);
        Assert.AreEqual(userEntity.Name, userDtoResult.Name);
        Assert.AreEqual(userEntity.Surname, userDtoResult.Surname);
        Assert.AreEqual(userEntity?.Patronymic, userDtoResult?.Patronymic);
        Assert.AreEqual(userEntity.UserName, userDtoResult.UserName);
        Assert.AreEqual(userEntity.Email, userDtoResult.Email);
    }
}