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
    public partial class ManageCategory : ComponentBase
    {
        [Parameter]
        public long PageId { get; set; }
        [Inject]
        NavigationManager AppNavManager { get; set; }
        [Inject]
        public IManageService<Category> DataService { get; set; }
        public string PageHeader { get; set; }
        public Category PageObj { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        long SubCatId;
        protected const string GetObjectServiceUrl = "CategorySvc/GetSingleCategory/";
        protected const string CreateServiceUrl = "CategorySvc/CreateCategory";
        protected const string UpdateServiceUrl = "CategorySvc/UpdateCategory";
        protected const string ListPageUrl = "/ExpCatList";
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (PageId != 0)
                {
                    PageHeader = "Edit Expense Category";
                    PageObj = await DataService.GetSingleAsync(GetObjectServiceUrl, PageId);
                    SubCatId = PageObj.ParentId;
                    var Categories = await DataService.GetAllAsync(ClientConstants.CatListSvcUrl);
                    CategoryList = Categories.Select(x => new Category { CategoryName = x.CategoryName, CategoryId = x.CategoryId });
                }
                else await ResetPage();
                StateHasChanged();
            }
        }


        private async Task ResetPage()
        {
            PageHeader = "Add Expense Category";
            PageObj = new Category();
            var Categories = await DataService.GetAllAsync(ClientConstants.CatListSvcUrl);
            CategoryList = Categories.Select(x => new Category { CategoryName = x.CategoryName, CategoryId = x.CategoryId });
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
            PageObj.ParentId = SubCatId;

        }

    }
}
