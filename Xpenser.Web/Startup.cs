using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xpenser.Models;
using Xpenser.UI;
using Xpenser.UI.Services;
using Xpenser.Web.ErrorLogging;

namespace Xpenser.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorise(options =>
                    {
                    options.ChangeTextOnKeyPress = true;
                    })
                    .AddBootstrapProviders()
                    .AddFontAwesomeIcons();
            services.AddRazorPages();
            services.AddServerSideBlazor()
                    .AddHubOptions(o => { o.MaximumReceiveMessageSize = 10 * 1024 * 1024; })
                    .AddCircuitOptions(options => { options.DetailedErrors = true; });
            services.AddSingleton<ILoggerManager, LoggerManager>();
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
            services.AddHttpClient<IManageService<Ledger>, ManageService<Ledger>>()
                    .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IManageService<ReccuringTransaction>, ManageService<ReccuringTransaction>>()
                   .AddHttpMessageHandler<ValidateHeaderHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
