using BankUI.Responses;
using BankUI.Services;
using Microsoft.AspNetCore.Components;

namespace BankUI.Pages
{
    partial class ViewAccount
    {
        [Inject]
        private IUserService? UserService { get; set; }

        [Inject]
        private IAccountService? AccountService { get; set; }

        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<UserAccountResponse> Accounts = new List<UserAccountResponse>();


        private bool _isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            _isInitialized = true;

            if (AuthenticationService != null && UserService != null)
            {
                var userId = await AuthenticationService.GetUserId();

                if (!string.IsNullOrEmpty(userId))
                {
                    var response = await UserService.GetUserAccountsById(userId);

                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (response.Data != null && response.Data != null && response.Data.Count > 0)
                        {
                            Accounts = response.Data;
                        }
                    }
                }
                else
                {
                    await AuthenticationService.LogoutAsync();
                    NavigationManager.NavigateTo("login", true);
                }
            }
        }

        protected async Task DeleteAccount(UserAccountResponse userAccountResponse) 
        {
            try
            {
            
                //after confirmation




            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task EditAccount(UserAccountResponse userAccountResponse)
        {
            try
            {

                //after confirmation




            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
