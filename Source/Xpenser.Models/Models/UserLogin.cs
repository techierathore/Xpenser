using System;
namespace Xpenser.Models.Models
{
	public class UserLogin
	{
		public int LoginId	{ get; set; }
		public long UserId	{ get; set; }
		public DateTime LoginDate	{ get; set; }
		public string LoginToken	{ get; set; }
		public string TokenStatus	{ get; set; }
		public DateTime ExipryDate	{ get; set; }
		public DateTime IssueDate	{ get; set; }
	}

	public enum TokenStatus
	{
		ValidToken,
		InValidToken,
		ExpiredToken,
		InActiveToken
	}
}
