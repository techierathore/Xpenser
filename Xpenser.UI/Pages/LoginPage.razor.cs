using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Xpenser.Models;
using Xpenser.UI.Services;

namespace Xpenser.UI.Pages
{
    public partial class LoginPage : ComponentBase
    {
        public SvcData LoginDetails { get; set; }
        public string LoginMesssage { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthService AuthSvc { get; set; }
        private AppUser vValidatedUser;
        ClaimsPrincipal PageClaimsPrincipal;

        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }

        protected async override Task OnInitializedAsync()
        {
            LoginDetails = new SvcData();
            vValidatedUser = new AppUser();

            PageClaimsPrincipal = (await AuthStateTask).User;
            if (PageClaimsPrincipal.Identity.IsAuthenticated)
            { NavigationManager.NavigateTo("/Index"); }
        }

        public async Task<bool> ValidateUser()
        {
            vValidatedUser = await AuthSvc.LoginAsync(LoginDetails);

            if (vValidatedUser.UserEmail != null)
            {
                await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(vValidatedUser);
                NavigationManager.NavigateTo("/Index");
            }
            else
            {
                LoginMesssage = "Invalid username or password";
            }
            return await Task.FromResult(true);
        }
    }
}
