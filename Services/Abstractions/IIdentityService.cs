using Models.DTO.IdentityDtos;
using Models.DTO.IdentityDtos.IdentityResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IIdentityService
    {
        Task<IdentityResponseRegister> RegisterUserAsync(RequestRegisterUserDto registration);

        Task<IdentityResponseLogin> LoginUserAsync(RequestLoginUserDto loginUser);
    }
}
