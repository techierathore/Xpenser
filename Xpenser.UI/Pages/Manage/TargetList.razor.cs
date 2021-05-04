using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Xpenser.Models;
using Xpenser.UI.Services;
namespace Xpenser.UI.Pages.Manage
{
    public partial class TargetList : ComponentBase
    {
        [Inject]
        public IManageService<Target> DataService { get; set; }
        public List<Target> ObjectList { get; set; }
        public Target SelObject { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ObjectList = await DataService.GetAllAsync(ClientConstants.TargetListSvcUrl);
        }
    }
}
