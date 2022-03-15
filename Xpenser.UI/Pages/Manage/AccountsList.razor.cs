using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Xpenser.Models;
using Xpenser.UI.Services;

namespace Xpenser.UI.Pages.Manage
{
    public partial class AccountsList : ComponentBase
    {
        [Inject]
        public IManageService<Account> DataService { get; set; }
        public List<Account> ObjectList { get; set; }
        public Account SelObject { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        protected override async Task OnInitializedAsync()
        {
            LoggedInUser = (await AuthStateTask).User;
            string vEmpId = LoggedInUser.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.PrimarySid)?.Value;
            long lEmployeeId = Convert.ToInt64(vEmpId);
            ObjectList = await DataService.GetAllSubsAsync(ClientConstants.UserAccSvcUrl, lEmployeeId);
        }
    }
}
