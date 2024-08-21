using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.IdentityDtos.IdentityResponse
{
    public class IdentityResponseLogin : IdentityResponse
    {
        public UserDto User { get; set; }
    }
}
