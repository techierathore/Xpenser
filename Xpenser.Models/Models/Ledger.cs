using System;

namespace Xpenser.Models
{
    public class Ledger
	{
		public long TransId	{ get; set; }
		public string TransName	{ get; set; }
		public string TransDescription	{ get; set; }
		public Double Amount { get; set; }
		public string TransType	{ get; set; }
		public long AppUserId	{ get; set; }
		public long CategoryId	{ get; set; }
		public long SrcAccId { get; set; }
		public string SrcAccName { get; set; }
		public long DestAccId { get; set; }
		public string DestAccName { get; set; }
		public string PicIds { get; set; }
	}
}
