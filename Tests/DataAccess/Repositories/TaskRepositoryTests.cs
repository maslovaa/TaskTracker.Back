using Tests.Builders;

namespace Tests.DataAccess.Repositories;

public class TaskRepositoryTests
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
    public async Task GetAll_ShouldReturnAllActiveTasks()
    {
        // Arrange

        int tasksCountResult;
        List<TaskEntity> tasks = new List<TaskEntity>()
        {
            TaskBuilder.CreateBaseTask(),
            TaskBuilder.CreateBaseTask(),
            TaskBuilder.CreateBaseTask().AsNoActive()
        };

        var expectedTasksCount = tasks.Where(u => u.IsActive).Count();

        using (var context = new DataContext(_options))
        {
            context.TaskEntities.AddRange(tasks);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act
            tasksCountResult = taskRepository.GetAll().Count();
        }

        // Assert

        Assert.That(expectedTasksCount, Is.EqualTo(tasksCountResult));
    }

    [Test]
    public async Task Add_ShouldReturnTaskId()
    {
        // Arrange

        var newTask = TaskBuilder.CreateBaseTask();
        Guid AddedTaskId;

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            AddedTaskId = taskRepository.Add(newTask);
            await context.SaveChangesAsync();

            // Assert

            Assert.That(newTask.Id, Is.EqualTo((await context.TaskEntities.FindAsync(AddedTaskId)).Id));
        }
    }

    [Test]
    public async Task AddAsync_ShouldAddedTaskId()
    {
        // Arrange

        var newTask = TaskBuilder.CreateBaseTask();
        Guid AddedTaskId;

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            AddedTaskId = await taskRepository.AddAsync(newTask);
            await context.SaveChangesAsync();

            // Assert

            Assert.That(newTask.Id,
                Is.EqualTo((await taskRepository.GetByIdAsync(AddedTaskId, CancellationToken.None)).Id));
        }
    }

    [Test]
    public async Task GetById_ShouldReturnTask()
    {
        // Arrange

        var task = TaskBuilder.CreateBaseTask();
        TaskEntity result;

        using (var context = new DataContext(_options))
        {
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            result = taskRepository.GetById(task.Id);
        }

        // Assert

        Assert.That(task.Id, Is.EqualTo(result.Id));
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnTask()
    {
        // Arrange

        var task = TaskBuilder.CreateBaseTask();
        TaskEntity result;

        using (var context = new DataContext(_options))
        {
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            result = await taskRepository.GetByIdAsync(task.Id, CancellationToken.None);
        }

        // Assert

        Assert.That(task.Id, Is.EqualTo(result.Id));
    }

    [Test]
    public async Task GetByPredicateAsync_ShouldReturnCollectionTasksByPredicate()
    {
        // Arrange

        var tasksList = new List<TaskEntity>()
        {
            TaskBuilder.CreateBaseTask().WithPerformer(),
            TaskBuilder.CreateBaseTask(),
            TaskBuilder.CreateBaseTask()
        };

        Expression<Func<TaskEntity, bool>> predicate = t => t.Performer != null;
        int expectedCount = tasksList.Where(t => t.Performer != null).Count();
        List<TaskEntity> result;

        using (var context = new DataContext(_options))
        {
            context.TaskEntities.AddRange(tasksList);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            result = new List<TaskEntity>(await taskRepository.GetByPredicateAsync(predicate, CancellationToken.None));
        }

        // Assert

        Assert.That(expectedCount, Is.EqualTo(result.Count));
    }

    [Test]
    public async Task Update_ShouldUpdateTaskInDataBase()
    {
        // Arrange

        bool updateResult;
        var task = TaskBuilder.CreateBaseTask().WithPerformer();
        using (var context = new DataContext(_options))
        {
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
        }

        task.Performer.UserName = "New Performer";

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            updateResult = await taskRepository.UpdateAsync(task, CancellationToken.None);

            // Assert

            Assert.That(updateResult, Is.True);
            Assert.That(task.Performer.UserName,
                Is.EqualTo((await taskRepository.GetByIdAsync(task.Id, CancellationToken.None)).Performer.UserName));
        }
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateTaskInDataBase()
    {
        // Arrange

        bool updateResult;
        var task = TaskBuilder.CreateBaseTask().WithPerformer();
        using (var context = new DataContext(_options))
        {
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
        }

        task.Performer.UserName = "New task performer";

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            updateResult = await taskRepository.UpdateAsync(task, CancellationToken.None);

            // Assert

            Assert.That(updateResult, Is.True);
            Assert.That(task.Performer.UserName,
                Is.EqualTo((await taskRepository.GetByIdAsync(task.Id, CancellationToken.None)).Performer.UserName));
        }
    }

    [Test]
    public async Task Delete_ShouldReturnTrue()
    {
        // Arrange
        var task = TaskBuilder.CreateBaseTask();
        var taskId = task.Id;
        using (var context = new DataContext(_options))
        {
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act
            var result = taskRepository.Delete(taskId);

            //Assert
            Assert.That(result, Is.True);
            Assert.That(await taskRepository.GetByIdAsync(taskId, CancellationToken.None), Is.Null);
        }
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnTrue()
    {
        // Arrange

        var task = TaskBuilder.CreateBaseTask();
        var taskId = task.Id;

        using (var context = new DataContext(_options))
        {
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
        }

        using (var context = new DataContext(_options))
        {
            var taskRepository = new TasksRepository(context);

            // Act

            var result = await taskRepository.DeleteAsync(taskId, CancellationToken.None);

            // Assert

            Assert.That(result, Is.True);
            Assert.That(await taskRepository.GetByIdAsync(taskId, CancellationToken.None), Is.Null);
        }
    }
}