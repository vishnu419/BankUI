using BankUI.Models;
using BankUI.Responses;
using System.Net.Http.Json;

namespace BankUI.Services
{
    /// <summary>
    ///Implements Account Related Operations 
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        ///Implements Account Related Operations 
        /// </summary>
        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerApi");
        }

        /// <summary>
        /// Get Account Details By Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Response<AccountResponse>?> GetAccountById(string accountId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Response<AccountResponse>>("api/Account/" + accountId + "");

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
