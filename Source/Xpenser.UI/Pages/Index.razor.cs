using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Xpenser.UI.Pages
{
    public class IndexBase : ComponentBase
    {

        public string PageHeader { get; set; }
        public WorkOrder PageObj { get; set; }
        protected string DefaultTabName = "JobInfo";
        protected string ClientDDLable = "Property Name";
        public IEnumerable<CreditCard> CreditCardList { get; set; }
        public IEnumerable<WorkOrder> WorkOrderList { get; set; }
        public IEnumerable<Employee> EmployeeList { get; set; }
        public IEnumerable<Employee> CrewManagerList { get; set; }
        public IEnumerable<Employee> CrewList { get; set; }
        protected Employee SelectedPendingOn;
        protected Employee SelectedSalesRep;
        protected Employee SelectedCrewManager;
        protected WorkOrder SelectedWorkOrder;
        protected CreditCard SelectedCreditCard;
        //protected List<CheckBoxList> Checkboxes = new List<CheckBoxList>();
        protected override void OnInitialized()
        {
            ResetPage();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                ResetPage();
                StateHasChanged();
            }
        }
        private void ResetPage()
        {
            PageHeader = "Add Work Order";
            PageObj = new WorkOrder();
            EmployeeList = MockService.GetEmployees();
            CrewManagerList = MockService.GetEmployees();
            WorkOrderList = MockService.GetWorkOrders();
            CreditCardList = MockService.GetCreditCards();
            StateHasChanged();
        }

        public void SaveData()
        {
            var vSelCreditCardID = SelectedCreditCard.CreditCardId;
            var vSelWoId = SelectedWorkOrder.WorkOrderId;
            PageObj.PendingOnEmpId = SelectedPendingOn.EmployeeId;
            PageObj.PendingOnEmail = SelectedPendingOn.Email;
            PageObj.CrewManager = SelectedCrewManager.EmployeeId;
        }

        public void CreditCardChangedHandler(object newValue)
        {
            var vSelectedVal = Convert.ToInt64(newValue);
            SelectedCreditCard = CreditCardList.First(i => i.CreditCardId == vSelectedVal);
            StateHasChanged();
        }
        public void PendingOnChangedHandler(object newValue)
        {
            var vSelectedVal = Convert.ToInt64(newValue);
            SelectedPendingOn = EmployeeList.First(i => i.EmployeeId == vSelectedVal);
            StateHasChanged();
        }
    }



}
