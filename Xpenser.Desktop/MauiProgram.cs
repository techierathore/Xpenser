using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Xpenser.Models;
using Xpenser.UI;
using Xpenser.UI.Services;

namespace Xpenser.Desktop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
                    .AddBootstrapProviders()
                    .AddFontAwesomeIcons();
            /*
            var appSettingSection = Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingSection);
            builder.Services.AddTransient<ValidateHeaderHandler>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddHttpClient<IAuthService, AuthService>();


            builder.Services.AddHttpClient<IManageService<Account>, ManageService<Account>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            builder.Services.AddHttpClient<IManageService<Category>, ManageService<Category>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            builder.Services.AddHttpClient<IManageService<Ledger>, ManageService<Ledger>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            builder.Services.AddHttpClient<IManageService<ReccuringTransaction>, ManageService<ReccuringTransaction>>()
                   .AddHttpMessageHandler<ValidateHeaderHandler>();
            */
            return builder.Build();
        }
    }
}