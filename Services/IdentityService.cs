using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Models.Dto;
using Models.DTO.IdentityDtos;
using Models.DTO.IdentityDtos.IdentityResponse;
using Services.Abstractions;
using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        private readonly string _identityUrl;

        public IdentityService(IHttpClientFactory httpClient, 
            IUserRepository userRepository, IMapper mapper,
            IConfiguration configuration)
        {
            _httpClient = httpClient.CreateClient();
            _userRepository = userRepository;
            _mapper = mapper;
            _identityUrl = configuration.GetValue<string>("AuthenticationService:IdentityUrl");
        }

        public async Task<IdentityResponseLogin> LoginUserAsync(RequestLoginUserDto loginUser)
        {
            var idnetityResponse = new IdentityResponseLogin();

            var user = (await _userRepository.GetByPredicateAsync(u => u.UserName == loginUser.UserName
                && u.IsActive == true, default)).FirstOrDefault();

            if (user is null)
            {
                idnetityResponse.ErrorMessage = "The user with this username does not exist or has been deleted.";

                return idnetityResponse;
            }

            var response = await _httpClient.PostAsJsonAsync($"{_identityUrl}/api/auth/login", loginUser);

            var responseObjectAsString = await response.Content.ReadAsStringAsync();

            var responseObject = JsonSerializer.Deserialize<IdentityResponse>(responseObjectAsString);

            if (response.IsSuccessStatusCode) 
            {

                if (string.IsNullOrEmpty(responseObject.Token)) 
                {
                    idnetityResponse.ErrorMessage = responseObject.ErrorMessage;

                    return idnetityResponse;
                }

                idnetityResponse.User = _mapper.Map<UserDto>(user);

                idnetityResponse.Token = responseObject.Token;

                idnetityResponse.UserId = responseObject.UserId;

                return idnetityResponse;
            }

            idnetityResponse.ErrorMessage = $"Authentication failed: {responseObject.ErrorMessage}";

            return idnetityResponse;

        }

        public async Task<IdentityResponseRegister> RegisterUserAsync(RequestRegisterUserDto registration)
        {

            var indetityResponse = new IdentityResponseRegister();
            
            if((await _userRepository.GetByPredicateAsync(u => u.UserName == registration.UserName,
                default)).Any()) 
            {
                indetityResponse.ErrorMessage = "the specified username is already occupied by another user.";

                return indetityResponse;
            }
            
            var response = await _httpClient.PostAsJsonAsync($"{_identityUrl}/api/auth/register", registration);

            var objectResponseAsString = await response.Content.ReadAsStringAsync();

            var objectResponse = JsonSerializer.Deserialize<IdentityResponse>(objectResponseAsString);

            if (response.IsSuccessStatusCode) 
            {
                if (string.IsNullOrEmpty(objectResponse.Token))
                {
                    indetityResponse.ErrorMessage = objectResponse.ErrorMessage;

                    return indetityResponse;
                }

                await _userRepository.AddAsync(_mapper.Map<UserEntity>(registration));

                indetityResponse.Token = objectResponse.Token;

                indetityResponse.UserId = objectResponse.UserId;

                return indetityResponse;
            }


            indetityResponse.ErrorMessage = $"Failed to register user: {objectResponse.ErrorMessage}";

            return indetityResponse;

        }
    }
}
