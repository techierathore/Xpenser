﻿using System;
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
    public partial class ManageAccounts : ComponentBase
    {
        [Parameter]
        public long PageId { get; set; }
        [Inject]
        NavigationManager AppNavManager { get; set; }
        [Inject]
        public IManageService<Account> DataService { get; set; }
        public string PageHeader { get; set; }
        public Account PageObj { get; set; }
        protected const string GetObjectServiceUrl = "AccSvc/GetSingleAccount/";
        protected const string CreateServiceUrl = "AccSvc/CreateAccount";
        protected const string UpdateServiceUrl = "AccSvc/UpdateAccount";
        protected const string ListPageUrl = "/AccList";
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        string SelectedAccountType;

        public List<AccountType> AccTypeList { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (PageId != 0)
                {
                    PageHeader = "Edit Account";
                    PageObj = await DataService.GetSingleAsync(GetObjectServiceUrl, PageId);
                    SelectedAccountType = PageObj.AcType;
                    AccTypeList = GetAccountTypes();
                }
                else ResetPage();
                StateHasChanged();
            }
        }


        private void ResetPage()
        {
            PageHeader = "Add Account";
            PageObj = new Account();
            AccTypeList = GetAccountTypes();
            StateHasChanged();
        }

        public async void SaveData()
        {
            PageObj.AcType = SelectedAccountType;
            if (PageId != 0)
            { _ = await DataService.UpdateAsync(UpdateServiceUrl, PageObj); }
            else
            {
                PageObj.StartDate = DateTime.Today;
                LoggedInUser = (await AuthStateTask).User;
                string vEmpId = LoggedInUser.Claims.FirstOrDefault(
                    c => c.Type == ClaimTypes.PrimarySid)?.Value;
                long lEmployeeId = Convert.ToInt64(vEmpId);
                PageObj.AppUserId = lEmployeeId;
                 _ = await DataService.SaveAsync(CreateServiceUrl, PageObj); }

            AppNavManager.NavigateTo(ListPageUrl);
        }
     
        public List<AccountType> GetAccountTypes()
        {
            List<AccountType> Accounts = new List<AccountType>() {
                new AccountType { AccountName = "Saving", AccounValue = "Saving" },
                new AccountType { AccountName = "Current", AccounValue = "Current" },
                new AccountType { AccountName = "Credit Card", AccounValue = "CreditCard" },
                new AccountType { AccountName = "Cash", AccounValue = "Cash" },
             };       
          
            return Accounts;
        }
    }
}
