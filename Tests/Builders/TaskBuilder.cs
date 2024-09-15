namespace Tests.Builders;

public static class TaskBuilder
{
    public static TaskEntity CreateBaseTask()
    {
        return new TaskEntity()
        {
            Id = Guid.NewGuid(),
            Ticket = "Base ticket.",
            Head = "Task head.",
            Body = "Task body.",
            Comment = "Comment for task.",
            Desk = null,
            Performer = null,
            IsActive = true
        };
    }

    public static TaskEntity AsNoActive(this TaskEntity task)
    {
        task.IsActive = false;
        return task;
    }

    public static TaskEntity WithDesk(this TaskEntity task)
    {
        task.Desk = new DeskEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Primary desk with tasks.",
            Project = null,
            Description = "Desk description.",
            Tasks = new List<TaskEntity>() { task },
            IsActive = true
        };
        return task;
    }

    public static TaskEntity WithPerformer(this TaskEntity task)
    {
        task.Performer = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Some User.",
            Surname = "Some Surname",
            Patronymic = "Some Patronymic.",
            Email = "basebox@domain.com",
            IsActive = true
        };

        return task;
    }
}