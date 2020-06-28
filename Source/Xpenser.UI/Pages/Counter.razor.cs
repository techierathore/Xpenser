using System;
using System.Collections.Generic;

namespace Xpenser.UI.Pages
{
    public class CounterBase
    {

    }

    public class Employee
    {

        public long EmployeeId
        { get; set; }

        public string FirstName
        { get; set; }

        public string LastName
        { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Address
        { get; set; }

        public string MobileNo
        { get; set; }

        public string Email
        { get; set; }

        public string Last4SSN
        { get; set; }

        public bool CompanyDriver
        { get; set; }

        public string DLState
        { get; set; }

        public string DLNumber
        { get; set; }

        public string DLClass
        { get; set; }

        public string EmployeeTier
        { get; set; }

        public string Gender
        { get; set; }

        public bool IsCrewManager
        { get; set; }

        public long ManagerId
        { get; set; }

        public string ManagerName
        { get; set; }

        public bool UniformIssued
        { get; set; }

        public string EmContactOne
        { get; set; }

        public string EmContactOneNo
        { get; set; }

        public string EmContactTwo
        { get; set; }

        public string EmContactTwoNo
        { get; set; }

        public string PasswordHash
        { get; set; }


        public string RfIdNo
        { get; set; }


        public string LoginName
        { get; set; }


        public string LoginPassword
        { get; set; }


        public string MobileRoles
        { get; set; }


        public string DefaultClass
        { get; set; }


        public long DefaultCreditCardID
        { get; set; }


        public int DefaultPropertyId
        { get; set; }


        public bool IsMobileAccessActive
        { get; set; }

        public bool CanMakeTimeCard
        { get; set; }

    }

    public class CreditCard
    {
        /// <summary>
        /// Gets or sets the CreditCardId value.
        /// </summary>
        public long CreditCardId
        { get; set; }

        /// <summary>
        /// Gets or sets the Last4Digits value.
        /// </summary>
        public string Last4Digits
        { get; set; }

        /// <summary>
        /// Gets or sets the ExpiryDate value.
        /// </summary>
        public DateTime ExpiryDate
        { get; set; }

        /// <summary>
        /// Gets or sets the ShortName value.
        /// </summary>
        public string ShortName
        { get; set; }

        /// <summary>
        /// Gets or sets the IsActive value.
        /// </summary>
        public bool IsActive
        { get; set; }

        /// <summary>
        /// Gets or sets the NameOnCard value.
        /// </summary>
        public string NameOnCard
        { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeId value.
        /// </summary>
        public long EmployeeId
        { get; set; }

    }

    public class WorkOrder
	{

		public long WorkOrderId
		{ get; set; }
		public long EstimateId
		{ get; set; }
		public string JobName
		{ get; set; }
		public string JobType
		{ get; set; }
		public int PropertyId
		{ get; set; }

		public string PropertyName
		{ get; set; }
		public long PendingOnEmpId
		{ get; set; }

		public string PendingOnEmail
		{ get; set; }

		public string WorkOrderClass
		{ get; set; }

		public bool MeetingDone
		{ get; set; }

		public string MeetingIds
		{ get; set; }

		public bool AnalyticsOn
		{ get; set; }

		public bool PermitRequired
		{ get; set; }

		public string PermitStatus
		{ get; set; }

		public string PermitDocsIds
		{ get; set; }

		public string PermitNotes
		{ get; set; }

		public string QBEstimateNo
		{ get; set; }

		public string WoDescription
		{ get; set; }

		public string WoDocsPicsIds
		{ get; set; }

		public DateTime? DeadlineDate
		{ get; set; }

		public TimeSpan? DeadlineTime
		{ get; set; }

		public DateTime? ScheduledDate
		{ get; set; }

		public string MaterialsUsed
		{ get; set; }

		public string HistoryAndLog
		{ get; set; }

		public long CrewManager
		{ get; set; }

		public string CrewMembersIds
		{ get; set; }

		public bool RescheduleJob
		{ get; set; }

		public Double AdditionalCosts
		{ get; set; }

		public string AdditionalCostDesc
		{ get; set; }

		public string CustomerName
		{ get; set; }

		public string CustomerPhoneNo
		{ get; set; }

	}

    public static class MockService
    {
        public static IEnumerable<WorkOrder> GetWorkOrders()
        {
            yield return new WorkOrder()
            {
                WorkOrderId = 246,
                JobName = "1240 Office Building",
                DeadlineDate = DateTime.Today.AddDays(10),
                PendingOnEmail = "techierathore@gmail.com"
            };
            yield return new WorkOrder()
            {
                WorkOrderId = 342,
                JobName = "Monthly Maintenance - Pest Control",
                DeadlineDate = DateTime.Today.AddDays(10),
                PendingOnEmail = "techierathore@gmail.com"
            };
            yield return new WorkOrder()
            {
                WorkOrderId = 347,
                JobName = "Weekly Maintenance - Weed Control, LeafRaking",
                DeadlineDate = DateTime.Today.AddDays(10),
                PendingOnEmail = "techierathore@gmail.com"
            };
            yield return new WorkOrder()
            {
                WorkOrderId = 349,
                JobName = "Monthly Maintenance - Weed Control, LeafRaking",
                DeadlineDate = DateTime.Today.AddDays(10),
                PendingOnEmail = "techierathore@gmail.com"
            };
            yield return new WorkOrder()
            {
                WorkOrderId = 446,
                JobName = "Antonio Friday (K.Barger - FreedomScientific)",
                DeadlineDate = DateTime.Today.AddDays(10),
                PendingOnEmail = "techierathore@gmail.com"
            };
            yield return new WorkOrder()
            {
                WorkOrderId = 456,
                JobName = "Service All Garden Equipments and Nursery",
                DeadlineDate = DateTime.Today.AddDays(10),
                PendingOnEmail = "techierathore@gmail.com"
            };

        }

        public static IEnumerable<CreditCard> GetCreditCards()
        {
            yield return new CreditCard()
            {
                CreditCardId = 41,
                ExpiryDate = DateTime.Today.AddYears(1),
                Last4Digits = "9112",
                NameOnCard = "David James",
                IsActive = true,
            };
            yield return new CreditCard()
            {
                CreditCardId = 42,
                ExpiryDate = DateTime.Today.AddMonths(9),
                Last4Digits = "9012",
                NameOnCard = "David James",
                IsActive = true,
            };
            yield return  new CreditCard()
            {
                CreditCardId = 44,
                ExpiryDate = DateTime.Today.AddYears(2),
                Last4Digits = "9102",
                NameOnCard = "David James",
                IsActive = true,
            };
            yield return new CreditCard()
            {
                CreditCardId = 46,
                ExpiryDate = DateTime.Today.AddMonths(11),
                Last4Digits = "0166",
                NameOnCard = "David James",
                IsActive = true,
            };
            yield return  new CreditCard()
            {
                CreditCardId = 21,
                ExpiryDate = DateTime.Today.AddYears(1),
                Last4Digits = "9142",
                NameOnCard = "S Ravi",
                IsActive = true,
            };
            yield return  new CreditCard()
            {
                CreditCardId = 22,
                ExpiryDate = DateTime.Today.AddMonths(9),
                Last4Digits = "9002",
                NameOnCard = "S Ravi",
                IsActive = true,
            };
            yield return  new CreditCard()
            {
                CreditCardId = 24,
                ExpiryDate = DateTime.Today.AddYears(2),
                Last4Digits = "2102",
                NameOnCard = "S Ravi",
                IsActive = true,
            };
            yield return  new CreditCard()
            {
                CreditCardId = 26,
                ExpiryDate = DateTime.Today.AddMonths(11),
                Last4Digits = "1466",
                NameOnCard = "S Ravi",
                IsActive = true,
            };
        }

        public static IEnumerable<Employee> GetEmployees()
        {
            yield return new Employee()
            {
                EmployeeId = 4,
                FirstName = "Ram",
                LastName = "Kumar",
                MobileNo = "9901691975",
                Email = "chrama50@gmail.com"
            };
            yield return new Employee()
            {
                EmployeeId = 6,
                FirstName = "Pankul",
                LastName = "Jain",
                MobileNo = "9001691905",
                Email = "PankulTest@gmail.com"
            };
            yield return new Employee()
            {
                EmployeeId = 7,
                FirstName = "Khusshbu",
                LastName = "Ojha",
                MobileNo = "9111691905",
                Email = "Khusshbu@gmail.com"
            };
            yield return new Employee()
            {
                EmployeeId = 9,
                FirstName = "Divya",
                LastName = "Bharti",
                MobileNo = "9111741925",
                Email = "DBharti@gmail.com"
            };
            yield return new Employee()
            {
                EmployeeId = 9,
                FirstName = "Mayuri",
                LastName = "Kango",
                MobileNo = "9111000025",
                Email = "MKango@gmail.com"
            };
            yield return new Employee()
            {
                EmployeeId = 11,
                FirstName = "Tamraj",
                LastName = "Kilwish",
                MobileNo = "9251000025",
                Email = "TKilwish@gmail.com"
            };
            yield return new Employee()
            {
                EmployeeId = 12,
                FirstName = "David",
                LastName = "James",
                MobileNo = "9521000025",
                Email = "DJames@gmail.com"
            };
        }
    }

}
