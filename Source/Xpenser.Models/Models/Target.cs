using System;

namespace Xpenser.Models
{
	public class Target
	{
		public long TargetId { get; set; }
		public string TargetTitle	{ get; set; }
		public string TargetDescription	{ get; set; }
		public long CategoryId	{ get; set; }
		public DateTime EntryDate	{ get; set; }
		public DateTime TargetDate	{ get; set; }
		public Double Amount	{ get; set; }
		public long AppUserId	{ get; set; }
	}
}
