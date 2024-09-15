using Domain.Entities;

namespace Tests.Builders;

public static class UserBuilder
{
    public static UserEntity CreateBaseUser()
    {
        return new UserEntity()
        {
            Id = Guid.NewGuid(),
            Name = "SomeName",
            Surname = "SomeSurname",
            Patronymic = "SomePatronymic",
            UserName = "SomeUserName",
            Email = "someMailAdress@domain.ru",
            IsActive = true,
            Projects = null,
            Role = null,
            Tasks = null
        };
    }

    public static UserEntity WithRole(this UserEntity user)
    {
        user.Role = new RoleEntity()
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Description = "Admin role",
            Users = new List<UserEntity>() { user }
        };
        
        return user;
    }

    public static UserEntity WithProjects(this UserEntity user)
    {
        user.Projects = new List<ProjectEntity>()
        {
            new ProjectEntity()
            {
                Id = Guid.NewGuid(),
                Name = "SomeProjectOne",
                Description = "SomeProjectOne details",
                StartDate = new DateTime(2024, 05, 10),
                EndDate = null,
                Status = "Active",
                Owner = user,
                Users = new List<UserEntity>(){user},
                Desks = new List<DeskEntity>()
            },
            new ProjectEntity()
            {
                Id = Guid.NewGuid(),
                Name = "SomeProjectTwo",
                Description = "SomeProjectTwo details",
                StartDate = new DateTime(2024, 04, 01),
                EndDate = new DateTime(2024, 05, 20),
                Status = "Completed",
                Owner = user,
                Users = new List<UserEntity>(){user},
                Desks = new List<DeskEntity>()
            }
        };

        return user;
    }

    public static UserEntity WithTasks(this UserEntity user)
    {
        user.Tasks = new List<TaskEntity>()
        {
            new TaskEntity()
            {
                Id = Guid.NewGuid(),
                Head = "Task One Head",
                Body = "Task One Body",
                Comment = "Task One comment",
                Performer = user
            },
            new TaskEntity()
            {
                Id = Guid.NewGuid(),
                Head = "Task Two Head",
                Body = "Task Two Body",
                Comment = "Task Two comment",
                Performer = user
            }
        };
        return user;
    }

    public static UserEntity AsInActive(this UserEntity user)
    {
        user.IsActive = false;
        return user;
    }
}