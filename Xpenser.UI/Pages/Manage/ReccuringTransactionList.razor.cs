using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Xpenser.Models;
using Xpenser.UI.Services;
namespace Xpenser.UI.Pages.Manage
{
    public partial class ReccuringTransactionList : ComponentBase
    {
        [Inject]
        public IManageService<ReccuringTransaction> DataService { get; set; }
        public List<ReccuringTransaction> ObjectList { get; set; }
        public ReccuringTransaction SelObject { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ObjectList = await DataService.GetAllAsync(ClientConstants.TransListSvcUrl);
        }
    }
}
