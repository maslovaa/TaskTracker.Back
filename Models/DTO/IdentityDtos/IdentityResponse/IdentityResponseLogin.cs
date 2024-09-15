using Models.Dto;

namespace Models.DTO.IdentityDtos.IdentityResponse
{
    public class IdentityResponseLogin : IdentityResponse
    {
        public UserDto User { get; set; }
    }
}
