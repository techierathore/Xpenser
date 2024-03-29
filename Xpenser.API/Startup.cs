using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Xpenser.API.Common;
using Xpenser.API.DaCore;
using Xpenser.API.DbAccess;
using Xpenser.API.ErrorLogging;
using Xpenser.Models;

namespace Xpenser.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            string sConString = Configuration["AppDbConString"];
        
            var vEmailConfigValues = new EmailSettings()
            {
                Host = Configuration["EmailHost"],
                Port = Convert.ToInt32(Configuration["EmailPort"]),
                UseSsl = Convert.ToBoolean(Configuration["EmailUseSsl"]),
                UserName = Configuration["EmailUser"],
                Password = Configuration["EmailPass"],
                SenderName = Configuration["EmailSenderName"],
                SenderAddress = Configuration["EmailSenderAddress"],
                CopyAddress = Configuration["EmailCopyAddress"]
            };
            var vAppSettings = new AppSettings()
            {
                WebAppBaseUrl = Configuration["WebAppBaseUrl"],
                SiteUrl = Configuration["SiteUrl"],
                HelpUrl = Configuration["HelpUrl"],
                AppName = Configuration["AppName"],
                CopyRightMessage = Configuration["CopyRightMessage"],
                FreeResumeCount = 2,
                EmailOptions = vEmailConfigValues
            };
            services.AddSingleton(vAppSettings);
            services.AddTransient<EmailService>();
            services.AddTransient<IUserLoginRepository>(x => new UserLoginRepo(sConString));
            services.AddTransient<IAppUserRepository>(x => new AppUserRepo(sConString));
            services.AddTransient<IAccountRepository>(x => new AccountRepo(sConString));
            services.AddTransient<ICategoryRepository>(x => new CategoryRepo(sConString));
            services.AddTransient<ILedgerRepository>(x => new LedgerRepo(sConString));
            services.AddTransient<IReccuringTransactionRepository>(x => new RecurringTransactionRepo(sConString));
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(AppConstants.JWTTokenGenKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "XpenserAPI" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XpenserAPI");
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
