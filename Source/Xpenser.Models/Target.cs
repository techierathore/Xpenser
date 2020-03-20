using System;
using System.Data;
using System.Data.Common;

namespace Xpenser.Models
{
	public class Target
	{
		#region Properties
		/// <summary>
		/// Gets or sets the TargetId value.
		/// </summary>
		public long TargetId
		{ get; set; }

		/// <summary>
		/// Gets or sets the TargetTitle value.
		/// </summary>
		public string TargetTitle
		{ get; set; }

		/// <summary>
		/// Gets or sets the TargetDescription value.
		/// </summary>
		public string TargetDescription
		{ get; set; }

		/// <summary>
		/// Gets or sets the CategiryId value.
		/// </summary>
		public long CategiryId
		{ get; set; }

		/// <summary>
		/// Gets or sets the EntryDate value.
		/// </summary>
		public DateTime EntryDate
		{ get; set; }

		/// <summary>
		/// Gets or sets the TargetDate value.
		/// </summary>
		public DateTime TargetDate
		{ get; set; }

		/// <summary>
		/// Gets or sets the Amount value.
		/// </summary>
		public Double Amount
		{ get; set; }

		/// <summary>
		/// Gets or sets the AppUserId value.
		/// </summary>
		public long AppUserId
		{ get; set; }

		#endregion
	}
}
