using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Xpenser.UI.Pages
{
    public class IndexBase : ComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthStateTask { get; set; }
        protected ClaimsPrincipal LoggedInUser;
        protected bool IsUserAuthenticated;

        protected override async Task OnInitializedAsync()
        {
            LoggedInUser = (await AuthStateTask).User;

            if (LoggedInUser.Identity.IsAuthenticated)
                IsUserAuthenticated = true;
        }
    }
}
