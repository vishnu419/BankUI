using BankUI.Services;
using Microsoft.AspNetCore.Components;

namespace BankUI.Pages
{
    public partial class Login
    {
        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        private CustomAuthenticationStateProvider? CustomAuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private LoginModel Model = new();

        public string? Message { get; set; }

        public async Task Submit()
        {
            try
            {
                if (Model != null && AuthenticationService != null && Model.UserName != null && Model.Password != null && CustomAuthenticationStateProvider != null)
                {
                    var result = await AuthenticationService.LoginAsync(Model.UserName, Model.Password);

                    if (result.Item1)
                    {
                        CustomAuthenticationStateProvider.NotifyUserAuthentication(result.Item2);
                        NavigationManager.NavigateTo("dashboard", true);
                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }

    public class LoginModel
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
