using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Xpenser.Models;
using Xpenser.UI.Services;
namespace Xpenser.UI.Pages.Manage
{
    public partial class CategoryList : ComponentBase
    {
        [Inject]
        public IManageService<Category> DataService { get; set; }
        public List<Category> ObjectList { get; set; }
        public Category SelObject { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ObjectList = await DataService.GetAllAsync(ClientConstants.CatListSvcUrl);
        }
    }
}
