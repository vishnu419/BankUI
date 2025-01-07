using BankUI.Responses;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BankUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _factory;

        private ILocalStorageService _localStorageService;

        private const string JWT_KEY = nameof(JWT_KEY);

        private string? _jwtCache;

        public event Action<string?>? LoginChange;

        public AuthenticationService(IHttpClientFactory factory, ILocalStorageService localStorageService) 
        {
            _factory = factory;
            _localStorageService = localStorageService;
        }

        public async ValueTask<string> GetJwtAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_jwtCache))
                    _jwtCache = await _localStorageService.GetItemAsync<string>(JWT_KEY);
            }
            catch (Exception)
            {

            }

            return _jwtCache;
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            var response = await _factory.CreateClient("ServerApi").DeleteAsync("api/authentication/revoke");

            await _localStorageService.RemoveItemAsync(JWT_KEY);

            _jwtCache = null;

            await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

            LoginChange?.Invoke(null);
        }

        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<(bool, string)> LoginAsync(string userName, string password)
        {
            var request = new
            {
                userName,
                password
            };

            var client = _factory.CreateClient("ServerApi");

            var response = await client.PostAsync("api/Login", JsonContent.Create(request));

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<LoginResponse>>();
                if (result != null && result.Errors != null)
                    throw new UnauthorizedAccessException(result.Errors[0].Message);

                throw new UnauthorizedAccessException();
            }

            var content = await response.Content.ReadFromJsonAsync<Response<LoginResponse>>();

            if (content == null || content.Data == null)
                throw new InvalidDataException();


            LoginChange?.Invoke(GetUsername(content.Data.Token));

            await _localStorageService.SetItemAsync<string>(JWT_KEY, content.Data.Token);

            return new(true, content.Data.Token);
        }

        /// <summary>
        /// Gets the logged in user Id
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserId()
        {
            if (string.IsNullOrEmpty(_jwtCache))
            {
                _jwtCache = await _localStorageService.GetItemAsync<string>(JWT_KEY);

                if (_jwtCache == null) 
                {
                    return String.Empty;
                }

                var jwt = new JwtSecurityToken(_jwtCache);

                return jwt.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            }

            else
            {
                return string.Empty;
            }
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);

            return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }
    }
}
