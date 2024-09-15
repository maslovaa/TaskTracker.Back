using Tests.Builders;

namespace Tests.Services.AutoMapper;

public class UserMapperTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });

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
        Assert.That(userDto.Name, Is.EqualTo(userResult.Name));
        Assert.That(userDto.Surname, Is.EqualTo(userResult.Surname));
        Assert.That(userDto?.Patronymic, Is.EqualTo(userResult?.Patronymic));
        Assert.That(userDto.UserName, Is.EqualTo(userResult.UserName));
        Assert.That(userDto.Email, Is.EqualTo(userResult.Email));
        
        
        Assert.That(userEntity.Name, Is.EqualTo(userDtoResult.Name));
        Assert.That(userEntity.Surname, Is.EqualTo(userDtoResult.Surname));
        Assert.That(userEntity?.Patronymic, Is.EqualTo(userDtoResult?.Patronymic));
        Assert.That(userEntity.UserName, Is.EqualTo(userDtoResult.UserName));
        Assert.That(userEntity.Email, Is.EqualTo(userDtoResult.Email));
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
        Assert.That(creatingUserDto.Name, Is.EqualTo(userResult.Name));
        Assert.That(creatingUserDto.Surname, Is.EqualTo(userResult.Surname));
        Assert.That(creatingUserDto?.Patronymic, Is.EqualTo(userResult?.Patronymic));
        Assert.That(creatingUserDto?.UserName, Is.EqualTo(userResult?.UserName));
        Assert.That(creatingUserDto?.Email, Is.EqualTo(userResult?.Email));
        Assert.That(userEntity.Name, Is.EqualTo(userDtoResult.Name));
        Assert.That(userEntity.Surname, Is.EqualTo(userDtoResult.Surname));
        Assert.That(userEntity?.Patronymic, Is.EqualTo(userDtoResult?.Patronymic));
        Assert.That(userEntity?.UserName, Is.EqualTo(userDtoResult?.UserName));
        Assert.That(userEntity?.Email, Is.EqualTo(userDtoResult?.Email));
    }
}