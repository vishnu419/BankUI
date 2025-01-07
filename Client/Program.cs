using BankUI.Handlers;
using BankUI.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
namespace BankUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddTransient<CustomAuthenticationStateProvider>();
            builder.Services.AddTransient<AuthenticationHandler>();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddHttpClient("ServerApi")
                            .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                            .AddHttpMessageHandler<AuthenticationHandler>();

            await builder.Build().RunAsync();
        }
    }
}
