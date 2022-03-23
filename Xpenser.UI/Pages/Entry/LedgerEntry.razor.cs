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
        [Inject]
        public IManageService<Account> AccDataService { get; set; }
        public string PageHeader { get; set; }
        public Ledger PageObj { get; set; }
        protected const string GetObjectServiceUrl = "LedgerSvc/GetSingleRecord/";
        protected const string CreateServiceUrl = "LedgerSvc/CreateRecord";
        protected const string UpdateServiceUrl = "LedgerSvc/UpdateRecord";
        protected const string ListPageUrl = "/AccList";
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        long SelAccount;
        public List<Account> UserAccList { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                UserAccList = await GetUserAccList();
                if (PageId != 0)
                {
                    PageHeader = "Edit " + TransType + " Entry";
                    PageObj = await DataService.GetSingleAsync(GetObjectServiceUrl, PageId);
                }
                else ResetPage();
                StateHasChanged();
            }
        }

        protected async Task<List<Account>> GetUserAccList()
        {
            LoggedInUser = (await AuthStateTask).User;
            string vEmpId = LoggedInUser.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.PrimarySid)?.Value;
            long lEmployeeId = Convert.ToInt64(vEmpId);
            var ObjectList = await AccDataService.GetAllSubsAsync(ClientConstants.UserAccSvcUrl, lEmployeeId);
            if (TransType == AppConstants.IncomeType)
            {
                ObjectList = ObjectList
                    .Where(l => l.AcType == AppConstants.AccTypeSaving 
                            || l.AcType == AppConstants.AccTypeCurrent)
                    .ToList();
            }
            return ObjectList;
        }

        private void ResetPage()
        {
            PageHeader = TransType + " Entry";
            PageObj = new Ledger();
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
                PageObj.TransType=TransType;
                PageObj.PicIds = " ";
                PageObj.SrcAccId = SelAccount;
                _ = await DataService.SaveAsync(CreateServiceUrl, PageObj);
            }

            AppNavManager.NavigateTo(ListPageUrl);
        }

    }
}
