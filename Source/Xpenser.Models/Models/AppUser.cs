namespace Xpenser.Models
{
	public class AppUser
	{
		public long AppUserId	{ get; set; }
		public string FirstName	{ get; set; }
		public string LastName	{ get; set; }
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}
		public string EmailID { get; set; }
		public string PasswordHash { get; set; }
		public string ConfirmPassword { get; set; }
		public string MobileNo	{ get; set; }
		public bool Verified { get; set; }
		public string Role	{ get; set; }
		public long ProfilePicId { get; set; }		
		public string ProfilePicPath { get; set; }
		public string AccessToken	{ get; set; }
		public string RefreshToken	{ get; set; }
	}
}
