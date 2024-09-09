using Models.Dto;
using System;
using System.Text.Json.Serialization;

namespace Models.DTO.IdentityDtos.IdentityResponse
{
    public class IdentityResponse
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
