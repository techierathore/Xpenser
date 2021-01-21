using System;

namespace Xpenser.Models
{
    public class ReccuringTransaction
	{
		public long ReccurTransId { get; set; }
		public string TransName	{ get; set; }
		public string TransDescription	{ get; set; }
		public Double Amount { get; set; }
		public string TransType	{ get; set; }
		public string Occurance	{ get; set; }
		public short DayOfMonth	{ get; set; }
		public long AppUserId	{ get; set; }
	}
}
