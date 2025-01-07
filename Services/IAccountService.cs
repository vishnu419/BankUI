using BankUI.Responses;

namespace BankUI.Services
{
    /// <summary>
    /// Account Services
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Gets Account Details By Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Response<AccountResponse>?> GetAccountById(string accountId);
    }
}