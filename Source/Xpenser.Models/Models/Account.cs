using System;

namespace Xpenser.Models
{
	public class Account
	{
		public long AccountId { get; set; }
		public string AcccountName	{ get; set; }
		public string AcNumber	{ get; set; }
		public Double OpenBal	{ get; set; }
		public string AcType	{ get; set; }
		public DateTime StartDate	{ get; set; }
		public long AppUserId	{ get; set; }
		public long IconPicId { get; set; }
		public string IconPath	{ get; set; }		
	}
}
