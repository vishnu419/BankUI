using BankUI.Models;
using BankUI.Responses;
using System.Net.Http.Json;

namespace BankUI.Services
{
    /// <summary>
    /// Implements User Service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerApi");
        }

        /// <summary>
        /// Gets User Details By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Response<UserResponse>?> GetUserById(string userId) 
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Response<UserResponse>>("api/User/"+ userId +"");

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Add Account To User Accounts List
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public async Task<UserAccountResponse?> AddUserAccount(UserAccount userAccount)
        {
            try
            {
                var request = JsonContent.Create(userAccount);

                var response = await _httpClient.PostAsync("api/user/addaccount", request, new CancellationToken());

                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response<UserAccountResponse>>();
                    if (result != null && result.Errors != null)
                        throw new Exception(result.Errors[0].Message);

                    throw new Exception();
                }

                var content = await response.Content.ReadFromJsonAsync<Response<UserAccountResponse>>();

                if (content == null || content.Data == null)
                    throw new InvalidDataException();

                return content.Data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets List of Accounts Added By User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Response<List<UserAccountResponse>>?> GetUserAccountsById(string userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Response<List<UserAccountResponse>>>("api/user/useraccounts?id=" + userId + "");

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
