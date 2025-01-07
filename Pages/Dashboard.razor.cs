using BankUI;
using BankUI.Services;
using Microsoft.AspNetCore.Components;

namespace BankUI.Pages
{
    public partial class Dashboard
    {
        [Inject]
        private IUserService? UserService { get; set; }

        [Inject]
        private IAccountService? AccountService { get; set; }

        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<AccountResponse> Accounts = new List<AccountResponse>();

        private Guid SelectedAccountId { get; set; }

        private decimal SelectedAccountBalance { get; set; }

        private string UserName { get; set; }

        private bool _isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            _isInitialized = true;

            if (AuthenticationService != null && UserService != null)
            {
                var userId = await AuthenticationService.GetUserId();

                if (!string.IsNullOrEmpty(userId))
                {
                    var response = await UserService.GetUserById(userId);

                    if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (response.Data != null)
                        {
                            UserName = response.Data.Username;
                        }

                        if (response.Data != null && response.Data.Accounts != null && response.Data.Accounts.Count > 0)
                        {
                            Accounts = response.Data.Accounts;
                            SelectedAccountBalance = Accounts[0].Balance;
                            SelectedAccountId = Accounts[0].AccountId;
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

        private async Task OnAccountChanged(ChangeEventArgs e)
        {
            try
            {
                if (Guid.TryParse(e.Value?.ToString(), out Guid newAccountId) && AccountService != null)
                {
                    var accountDetails = await AccountService.GetAccountById(newAccountId.ToString());

                    if (accountDetails != null && accountDetails.Data != null) 
                    {
                        SelectedAccountId = newAccountId;
                        SelectedAccountBalance = accountDetails.Data.Balance;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
