using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using Xpenser.Models;
using Xpenser.UI;
using Xpenser.UI.Services;

namespace BlazorDesk
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
        .AddBootstrapProviders()
        .AddFontAwesomeIcons();
            /*
var appSettingSection = Configuration.GetSection("AppSettings");
services.Configure<AppSettings>(appSettingSection);
services.AddTransient<ValidateHeaderHandler>();
services.AddBlazoredLocalStorage();

services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
services.AddHttpClient<IAuthService, AuthService>();

services.AddHttpClient<IManageService<Account>, ManageService<Account>>()
        .AddHttpMessageHandler<ValidateHeaderHandler>();
services.AddHttpClient<IManageService<Category>, ManageService<Category>>()
        .AddHttpMessageHandler<ValidateHeaderHandler>();
services.AddHttpClient<IManageService<ReccuringTransaction>, ManageService<ReccuringTransaction>>()
       .AddHttpMessageHandler<ValidateHeaderHandler>();

*/
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
