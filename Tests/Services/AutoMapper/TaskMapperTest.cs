using Tests.Builders;

namespace Tests.Services.AutoMapper;

public class TaskMapperTest
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });

        _mapper = config.CreateMapper();
    }

    [Test]
    public void Map_WhenGivenValidSourceObjectTaskDtoOrTaskEntity()
    {
        // Arrange

        var taskDto = new TaskDto()
        {
            Id = Guid.NewGuid(),
            Ticket = "SomeTicket",
            Head = "Task head",
            Body = "Task body",
            Comment = "Task comment"
        };

        var taskEntity = TaskBuilder.CreateBaseTask();

        // Act
        var taskResult = _mapper.Map<TaskEntity>(taskDto);
        var taskDtoResult = _mapper.Map<TaskDto>(taskEntity);

        // Assert
        Assert.That(taskDto.Id, Is.EqualTo(taskResult.Id));
        Assert.That(taskDto.Ticket, Is.EqualTo(taskResult.Ticket));
        Assert.That(taskDto.Head, Is.EqualTo(taskResult.Head));
        Assert.That(taskDto.Body, Is.EqualTo(taskResult.Body));
        Assert.That(taskDto.Comment, Is.EqualTo(taskResult.Comment));

        Assert.That(taskEntity.Id, Is.EqualTo(taskDtoResult.Id));
        Assert.That(taskEntity.Ticket, Is.EqualTo(taskDtoResult.Ticket));
        Assert.That(taskEntity.Head, Is.EqualTo(taskDtoResult.Head));
        Assert.That(taskEntity.Body, Is.EqualTo(taskDtoResult.Body));
        Assert.That(taskEntity.Comment, Is.EqualTo(taskDtoResult.Comment));
    }
}