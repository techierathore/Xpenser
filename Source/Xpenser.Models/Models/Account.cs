using System;

namespace Xpenser.Models
{
	public class Account
	{
		#region Properties
		/// <summary>
		/// Gets or sets the AccountId value.
		/// </summary>
		public long AccountId
		{ get; set; }

		/// <summary>
		/// Gets or sets the AcccountName value.
		/// </summary>
		public string AcccountName
		{ get; set; }

		/// <summary>
		/// Gets or sets the AcNumber value.
		/// </summary>
		public string AcNumber
		{ get; set; }

		/// <summary>
		/// Gets or sets the OpenBal value.
		/// </summary>
		public Double OpenBal
		{ get; set; }

		/// <summary>
		/// Gets or sets the AcType value.
		/// </summary>
		public string AcType
		{ get; set; }

		/// <summary>
		/// Gets or sets the StartDate value.
		/// </summary>
		public DateTime StartDate
		{ get; set; }

		/// <summary>
		/// Gets or sets the AppUserId value.
		/// </summary>
		public long AppUserId
		{ get; set; }

		/// <summary>
		/// Gets or sets the IconPath value.
		/// </summary>
		public string IconPath
		{ get; set; }

		#endregion
	}
}
