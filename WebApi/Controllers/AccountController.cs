using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.IdentityDtos;
using Services.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RequestRegisterUserDto registrationModel) 
        {
            var tokenResponse = await _identityService.RegisterUserAsync(registrationModel);

            if(tokenResponse.Token is null) 
            {
                return BadRequest(tokenResponse.ErrorMessage);
            }

            return Ok(tokenResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(RequestLoginUserDto loginModel) 
        {
            var loginResponse = await _identityService.LoginUserAsync(loginModel);

            if (loginResponse.Token is null) 
            {
                return BadRequest(loginResponse.ErrorMessage);
            }

            return Ok(loginResponse);
        }

        [Authorize]
        [HttpGet("isAuth")]
        public IActionResult IsAuth()
        {
            return Ok(true); 
        }
    }
}
