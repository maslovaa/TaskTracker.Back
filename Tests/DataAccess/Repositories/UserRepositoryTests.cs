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
            var result = userRepository.Delete(userId);
            
            //Assert
            Assert.That(result, Is.True);
            Assert.That(await userRepository.GetByIdAsync(userId, CancellationToken.None), Is.Null);
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

            var result = userRepository.Delete(randomId);
            
            // Assert
            Assert.That(result, Is.False);
        }
    }

    [Test]
    public async Task DeleteAsync_UserExist_ShouldReturnTrue()
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

            var result = await userRepository.DeleteAsync(userId, CancellationToken.None);
            
            // Assert
            
            Assert.That(result, Is.True);
            Assert.That(await userRepository.GetByIdAsync(userId, CancellationToken.None), Is.Null);
        }
    }

    [Test]
    public async Task GetAll_ShouldReturnAllActiveUsers()
    {
        // Arrange

        int usersCountResult;
        List<UserEntity> users = new List<UserEntity>()
        {
            UserBuilder.CreateBaseUser(),
            UserBuilder.CreateBaseUser(),
            UserBuilder.CreateBaseUser().AsInActive()
        };
        
        var expectedUsersCount = users.Where(u => u.IsActive).Count();

        using (var context = new DataContext(_options))
        {
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act
            usersCountResult = userRepository.GetAll().Count();
        }
        
        // Assert
        
        Assert.That(expectedUsersCount, Is.EqualTo(usersCountResult));
    }

    [Test]
    public async Task Add_ShouldAddedUserId()
    {
        // Arrange
        
        var newUser = UserBuilder.CreateBaseUser();
        Guid AddedUserId;
        
        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            AddedUserId = userRepository.Add(newUser);
            await context.SaveChangesAsync();
            
            // Assert
            
            Assert.That(newUser.Id, Is.EqualTo((await context.Users.FindAsync(AddedUserId)).Id));
        }
    }

    [Test]
    public async Task AddAsync_ShouldAddedUserId()
    {
        // Arrange
        
        var newUser = UserBuilder.CreateBaseUser();
        Guid AddedUserId;
        
        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            AddedUserId = await userRepository.AddAsync(newUser);
            await context.SaveChangesAsync();
            
            // Assert
            
            Assert.That(newUser.Id, Is.EqualTo((await userRepository.GetByIdAsync(AddedUserId, CancellationToken.None)).Id));
        }
    }

    [Test]
    public async Task GetById_ShouldReturnUser()
    {
        // Arrange
        
        var user = UserBuilder.CreateBaseUser();
        UserEntity result;
        
        using (var context = new DataContext(_options))
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            result = userRepository.GetById(user.Id);
        }
        
        // Assert
        
        Assert.That(user.Id, Is.EqualTo(result.Id));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnUser()
    {
        // Arrange
        
        var user = UserBuilder.CreateBaseUser();
        UserEntity result;
        
        using (var context = new DataContext(_options))
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            result = await userRepository.GetByIdAsync(user.Id, CancellationToken.None);
        }
        
        // Assert
        
        Assert.That(user.Id, Is.EqualTo(result.Id));
    }

    [Test]
    public async Task GetByPredicateAsync_ShouldReturnCollectionUsersByPredicate()
    {
        // Arrange
        
        var usersList = new List<UserEntity>()
        {
            UserBuilder.CreateBaseUser().WithTasks(),
            UserBuilder.CreateBaseUser().WithTasks(),
            UserBuilder.CreateBaseUser()
        };

        Expression<Func<UserEntity, bool>> predicate = u => u.Tasks.Any();
        int expectedCount = usersList.Where(u => u.Tasks != null).Count();
        List<UserEntity> result;
        
        using (var context = new DataContext(_options))
        {
            context.Users.AddRange(usersList);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            result = new List<UserEntity>(await userRepository.GetByPredicateAsync(predicate, CancellationToken.None));
        }
        
        // Assert
        
        Assert.That(expectedCount, Is.EqualTo(result.Count));
    }

    [Test]
    public async Task UpdateAsync_UserExist_ShouldUpdateUserInDataBase()
    {
        // Arrange

        bool updateResult; 
        var user = UserBuilder.CreateBaseUser().WithRole();
        using (var context = new DataContext(_options))
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        user.Role.Name = "Executer";

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            updateResult = await userRepository.UpdateAsync(user, CancellationToken.None);
            
            // Assert
            
            Assert.That(updateResult, Is.True);
            Assert.That(user.Role.Name, Is.EqualTo((await userRepository.GetByIdAsync(user.Id, CancellationToken.None)).Role.Name));
        }
    }

    [Test]
    public async Task Update_UserExist_ShouldUpdateUserInDataBase()
    {
        // Arrange

        bool updateResult; 
        var user = UserBuilder.CreateBaseUser().WithRole();
        using (var context = new DataContext(_options))
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        user.Role.Name = "Author";

        using (var context = new DataContext(_options))
        {
            var userRepository = new UserRepository(context);
            
            // Act

            updateResult = await userRepository.UpdateAsync(user, CancellationToken.None);
            
            // Assert
            
            Assert.That(updateResult, Is.True);
            Assert.That(user.Role.Name, Is.EqualTo((await userRepository.GetByIdAsync(user.Id, CancellationToken.None)).Role.Name));
        }
    }
}