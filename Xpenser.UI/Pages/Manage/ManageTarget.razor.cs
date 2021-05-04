using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xpenser.UI.Pages.Manage
{
    public partial class ManageTarget : ComponentBase
    {
        [Parameter]
        public long PageId { get; set; }
        [Inject]
        NavigationManager AppNavManager { get; set; }
        [Inject]
        public IManageService<Target> DataService { get; set; }
        public string PageHeader { get; set; }
        public Target PageObj { get; set; }
        public IEnumerable<Category> CategoryListData { get; set; }

        long SubCatId;
        protected const string GetObjectServiceUrl = "TargetSvc/GetSingleTarget/";
        protected const string CreateServiceUrl = "TargetSvc/CreateTarget";
        protected const string UpdateServiceUrl = "TargetSvc/UpdateTarget";
        protected const string ListPageUrl = "/TargetList";
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        ClaimsPrincipal LoggedInUser;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (PageId != 0)
                {
                    PageHeader = "Edit Target";
                    PageObj = await DataService.GetSingleAsync(GetObjectServiceUrl, PageId);
                    SubCatId = PageObj.ParentId;
                    var Categories = await DataService.GetAllAsync(ClientConstants.CatListSvcUrl);
                    CategoryListData = Categories.Select(x => new Category { CategoryName = x.CategoryName, CategoryId = x.CategoryId });
                }
                else await ResetPage();
                StateHasChanged();
            }
        }

        private async Task ResetPage()
        {
            PageHeader = "Add Target";
            PageObj = new Target();
            var Categories = await DataService.GetAllAsync(ClientConstants.CatListSvcUrl);
            CategoryListData = Categories.Select(x => new Category { CategoryName = x.CategoryName, CategoryId = x.CategoryId });
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

        public async void OnCatagoryChanged(ChangeEventArgs e)
        {
            SubCatId = e.Value;
            PageObj.CategoryId = SubCatId;
        }
    }
}
