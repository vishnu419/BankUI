
namespace BankUI.Services
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;

        ValueTask<string> GetJwtAsync();

        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        Task<(bool, string)> LoginAsync(string userName, string password);

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        /// <summary>
        /// Gets the logged in user Id
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserId();
    }
}