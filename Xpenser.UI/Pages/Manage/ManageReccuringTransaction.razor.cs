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
    public partial class ManageReccuringTransaction : ComponentBase
    {
        [Parameter]
        public long PageId { get; set; }
        [Inject]
        NavigationManager AppNavManager { get; set; }
        [Inject]
        public IManageService<ReccuringTransaction> DataService { get; set; }
        public string PageHeader { get; set; }
        public ReccuringTransaction PageObj { get; set; }
        public IEnumerable<ReccuringTransaction> ReccuringTransactionsList { get; set; }

        protected const string GetObjectServiceUrl = "ReccurringTransactionSvc/GetSingleReccuringTransaction/";
        protected const string CreateServiceUrl = "ReccurringTransactionSvc/CreateReccuringTransaction";
        protected const string UpdateServiceUrl = "ReccurringTransactionSvc/UpdateReccuringTransaction";
        protected const string ListPageUrl = "/FixedTransList";
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        long SubCatId;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (PageId != 0)
                {
                    PageHeader = "Edit Reccuring Transaction";
                    PageObj = await DataService.GetSingleAsync(GetObjectServiceUrl, PageId);
                    SubCatId = PageObj.ReccurTransId;
                    var ReccuringTransactions = await DataService.GetAllAsync(ClientConstants.TransListSvcUrl);
                    ReccuringTransactionsList = ReccuringTransactionsList.Select(x => new ReccuringTransaction { TransName = x.TransName, ReccurTransId = x.ReccurTransId });
                }
                else await ResetPage();
                StateHasChanged();
            }
        }

        private async Task ResetPage()
        {
            PageHeader = "Add New Recurring Transaction";
            PageObj = new ReccuringTransaction();
            var reccuringTransactions = await DataService.GetAllAsync(ClientConstants.TransListSvcUrl);
            ReccuringTransactionsList = reccuringTransactions.Select(x => new ReccuringTransaction { TransName = x.TransName, ReccurTransId = x.ReccurTransId });
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
                // PageObj.ParentId = 0;
                _ = await DataService.SaveAsync(CreateServiceUrl, PageObj);
            }

            AppNavManager.NavigateTo(ListPageUrl);
        }
        void MyListValueChangedHandler(long newValue)
        {
            //SubCatId = Convert.ToInt64(newValue);
            PageObj.ReccurTransId = SubCatId;

        }

    }
}
