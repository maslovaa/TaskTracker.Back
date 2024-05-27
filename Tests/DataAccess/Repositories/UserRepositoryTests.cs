using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Builders;

namespace Tests.DataAccess.Repositories;

public class UserRepositoryTests
{
    private DbContextOptions<DataContext> _options;

    [SetUp]
    public void SetUp()
    {
        _options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using (var context = new DataContext(_options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }

    [Test]
    public async Task Delete_UserExist_ShouldReturnTrue()
    {
        // Arrange
        var user = UserBuilder.CreateBaseUser();
        var userId = user.Id;
        using (var context = new DataContext(_options))
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act
            var result = await userRepository.Delete(userId, CancellationToken.None);
            
            //Assert
            Assert.IsTrue(result);
            Assert.IsFalse((await userRepository.GetByIdAsync(userId, CancellationToken.None)).IsActive);
        }
    }

    [Test]
    public async Task Delete_UserNotExist_ShouldReturnFalse()
    {
        // Arrange
        var user = UserBuilder.CreateBaseUser();
        var randomId = Guid.NewGuid();
        using (var context = new DataContext(_options))
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            var result = await userRepository.Delete(randomId, CancellationToken.None);
            
            // Assert
            Assert.IsFalse(result);
        }
    }

    [Test]
    public async Task Delete_DeletedUser_ShouldReturnFalse()
    {
        // Arrange
        var user = UserBuilder.CreateBaseUser().AsInActive();

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act
            var result = userRepository.Delete(user);
            
            // Assert
            Assert.IsFalse(result);
        }
    }
}