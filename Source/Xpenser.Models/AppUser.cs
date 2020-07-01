using System;
using System.Data;
using System.Data.Common;

namespace Xpenser.Models
{
	public class AppUser
	{

		public long AppUserId
		{ get; set; }

		/// <summary>
		/// Gets or sets the FirstName value.
		/// </summary>
		public string FirstName
		{ get; set; }

		/// <summary>
		/// Gets or sets the LastName value.
		/// </summary>
		public string LastName
		{ get; set; }

		/// <summary>
		/// Gets or sets the EmailID value.
		/// </summary>
		public string EmailID
		{ get; set; }

		/// <summary>
		/// Gets or sets the LoginPassword value.
		/// </summary>
		public string LoginPassword
		{ get; set; }

		/// <summary>
		/// Gets or sets the MobileNo value.
		/// </summary>
		public string MobileNo
		{ get; set; }

		/// <summary>
		/// Gets or sets the Verified value.
		/// </summary>
		public bool Verified
		{ get; set; }

		/// <summary>
		/// Gets or sets the Role value.
		/// </summary>
		public string Role
		{ get; set; }

		/// <summary>
		/// Gets or sets the ProfilePicPath value.
		/// </summary>
		public string ProfilePicPath
		{ get; set; }

	}
}
