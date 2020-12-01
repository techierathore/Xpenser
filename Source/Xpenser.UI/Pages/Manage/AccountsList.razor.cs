using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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
        protected override async Task OnInitializedAsync()
        {
            ObjectList = await DataService.GetAllAsync(ClientConstants.AccListSvcUrl);
        }
    }
}
