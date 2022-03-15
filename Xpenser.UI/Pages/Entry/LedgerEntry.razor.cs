using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xpenser.Models;
using Xpenser.UI.Services;

namespace Xpenser.UI.Pages.Entry
{
    public partial class LedgerEntry : ComponentBase
    {
        [Parameter]
        public string TransType { get; set; }
        [Parameter]
        public long PageId { get; set; }
        [Inject]
        NavigationManager AppNavManager { get; set; }
        [Inject]
        public IManageService<Ledger> DataService { get; set; }
        public string PageHeader { get; set; }
        public Ledger PageObj { get; set; }
        protected const string GetObjectServiceUrl = "LedgerSvc/GetSingleRecord/";
        protected const string CreateServiceUrl = "LedgerSvc/CreateRecord";
        protected const string UpdateServiceUrl = "LedgerSvc/UpdateRecord";
        protected const string ListPageUrl = "/AccList";
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        long SelSrcAcc, SelDstAcc;
        public List<Account> UserAccList { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (PageId != 0)
                {
                    PageHeader = "Edit " + TransType + " Entry";
                    UserAccList = new List<Account>();
                    PageObj = await DataService.GetSingleAsync(GetObjectServiceUrl, PageId);
                }
                else ResetPage();
                StateHasChanged();
            }
        }


        private void ResetPage()
        {
            PageHeader = TransType + " Entry";
            PageObj = new Ledger();
            UserAccList = new List<Account>();
            StateHasChanged();
        }

        public async void SaveData()
        {

            if (PageId != 0)
            { _ = await DataService.UpdateAsync(UpdateServiceUrl, PageObj); }
            else
            {

                LoggedInUser = (await AuthStateTask).User;
                string vEmpId = LoggedInUser.Claims.FirstOrDefault(
                    c => c.Type == ClaimTypes.PrimarySid)?.Value;
                long lEmployeeId = Convert.ToInt64(vEmpId);
                PageObj.AppUserId = lEmployeeId;
                _ = await DataService.SaveAsync(CreateServiceUrl, PageObj);
            }

            AppNavManager.NavigateTo(ListPageUrl);
        }

    }
}
