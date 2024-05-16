using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Services.Abstractions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserEntityService _userService) : ControllerBase
{
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