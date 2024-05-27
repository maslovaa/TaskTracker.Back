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
    public void Map_WhenGivenValidSourceObject_ReturnsValidOriginObject()
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
}