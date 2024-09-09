using Models.DTO.IdentityDtos;
using Models.DTO.IdentityDtos.IdentityResponse;

namespace Services.Abstractions
{
    public interface IIdentityService
    {
        Task<IdentityResponseRegister> RegisterUserAsync(RequestRegisterUserDto registration);

        Task<IdentityResponseLogin> LoginUserAsync(RequestLoginUserDto loginUser);
    }
}
