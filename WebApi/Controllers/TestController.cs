using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetUser")]
    public User GetUserModel()
    {
        return new User()
        {
            Name = "SomeName",
            Surname = "SomeSurname",
            Patronymic = "SomePatronymic",
            Email = "mock@domain.ru",
            Id = 1,
            IsActive = true,
            UserName = "SomeUserName"
        };
    }
}