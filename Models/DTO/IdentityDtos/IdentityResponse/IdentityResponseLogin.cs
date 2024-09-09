using Models.Dto;

namespace Models.DTO.IdentityDtos.IdentityResponse
{
    public class IdentityResponseLogin : IdentityResponse
    {
        public Guid UserId { get; set; }

        public UserDto User { get; set; }
    }
}
