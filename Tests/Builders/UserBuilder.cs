using Domain.Entities;

namespace Tests.Builders;

public static class UserBuilder
{
    public static UserEntity CreateBaseUser()
    {
        return new UserEntity()
        {
            Id = Guid.Parse("7d994823-8226-4273-b063-1a95f3cc1df8"),
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
            Id = Guid.Parse("6adc3071-fe19-412a-875b-ffb617dc3a56"),
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
                Id = Guid.Parse("bb64e43e-1132-46dc-826e-b58def0a875e"),
                Name = "SomeProjectOne",
                Description = "SomeProjectOne details",
                StartDate = new DateTime(2024, 05, 10),
                EndDate = null,
                Status = "Active",
                Owner = user,
                Desks = new List<DeskEntity>()
            },
            new ProjectEntity()
            {
                Id = Guid.Parse("08fc1ab3-3077-4710-97eb-daf183212b92"),
                Name = "SomeProjectTwo",
                Description = "SomeProjectTwo details",
                StartDate = new DateTime(2024, 04, 01),
                EndDate = new DateTime(2024, 05, 20),
                Status = "Completed",
                Owner = user,
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
                Id = Guid.Parse("d3f03ccf-3958-466e-8bf7-9cd09b5e6018"),
                Head = "Task One Head",
                Body = "Task One Body",
                Comment = "Task One comment",
                Performer = user
            },
            new TaskEntity()
            {
                Id = Guid.Parse("225b8102-6f75-44c0-a4f4-420f60674d9e"),
                Head = "Task Two Head",
                Body = "Task Two Body",
                Comment = "Task Two comment",
                Performer = user
            }
        };
        return user;
    }

    public static UserEntity AsInActive(UserEntity user)
    {
        user.IsActive = false;
        return user;
    }
}