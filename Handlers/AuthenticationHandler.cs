using BankUI.Services;
using System.Net.Http.Headers;

namespace BankUI.Handlers
{
    /// <summary>
    /// AuthenticationHandler
    /// </summary>
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// AuthenticationHandler Constructor
        /// </summary>
        /// <param name="authenticationService"></param>
        /// <param name="configuration"></param>
        public AuthenticationHandler(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        /// <summary>
        /// Send Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var jwt = await _authenticationService.GetJwtAsync();
                var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

                if (isToServer && !string.IsNullOrEmpty(jwt))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var response = await base.SendAsync(request, cancellationToken);

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
