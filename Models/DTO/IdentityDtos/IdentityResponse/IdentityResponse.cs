using Models.Dto;
using System;
using System.Text.Json.Serialization;

namespace Models.DTO.IdentityDtos.IdentityResponse
{
    public class IdentityResponse
    {
        public Guid UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
