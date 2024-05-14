using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models.Dto;
using Services.Abstractions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserEntityService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserEntityService userService, IMapper mapper, ILogger<UsersController> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatingUserDto creatingUserDto)
    {
        return Ok(await _userService.CreateAsync(creatingUserDto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditAsync(Guid id, UserDto updatingUserDto)
    {
        await _userService.UpdateAsync(id, updatingUserDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid guid)
    {
        await _userService.DeleteAsync(guid);
        return Ok();
    }
}