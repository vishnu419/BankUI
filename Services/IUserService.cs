using BankUI.Models;
using BankUI.Responses;

namespace BankUI.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets User Details By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response<UserResponse>?> GetUserById(string userId);

        /// <summary>
        /// Add Account To User Accounts List
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        Task<UserAccountResponse?> AddUserAccount(UserAccount userAccount);

        /// <summary>
        /// Gets List of Accounts Added By User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response<List<UserAccountResponse>>?> GetUserAccountsById(string userId);
    }
}