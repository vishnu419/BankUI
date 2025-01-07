using BankUI.Models;
using BankUI.Services;
using Microsoft.AspNetCore.Components;

namespace BankUI.Pages
{
    partial class AddAccount
    {
        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        private IUserService? UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string Message { get; set; }

        private UserAccount Model = new ();

        private async Task Submit()
        {
            try
            {
                if (Model != null && AuthenticationService != null && UserService != null)
                {
                    var userId = await AuthenticationService.GetUserId();

                    if (string.IsNullOrEmpty(userId))
                    {
                        NavigationManager.NavigateTo("login", true);
                    }

                    if (Guid.TryParse(userId, out var loggedInUserId))
                        Model.UserId = loggedInUserId;

                    Model.UserAccountId = String.Empty;
                    var response = await UserService.AddUserAccount(Model);

                    if (response != null) 
                    {
                        Message = "Account added successfully..!!";
                        Model = new UserAccount();
                    }
                }
                else
                    return;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
