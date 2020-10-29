using System;
using System.Data;
using System.Data.Common;

namespace Xpenser.Models
{
	public class ReccuringTransaction
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ReccuringTransId value.
		/// </summary>
		public long ReccuringTransId
		{ get; set; }

		/// <summary>
		/// Gets or sets the TransName value.
		/// </summary>
		public string TransName
		{ get; set; }

		/// <summary>
		/// Gets or sets the TransDescription value.
		/// </summary>
		public string TransDescription
		{ get; set; }

		/// <summary>
		/// Gets or sets the Amount value.
		/// </summary>
		public Double Amount
		{ get; set; }

		/// <summary>
		/// Gets or sets the TransType value.
		/// </summary>
		public string TransType
		{ get; set; }

		/// <summary>
		/// Gets or sets the Occurance value.
		/// </summary>
		public string Occurance
		{ get; set; }

		/// <summary>
		/// Gets or sets the DayOfMonth value.
		/// </summary>
		public short DayOfMonth
		{ get; set; }

		/// <summary>
		/// Gets or sets the AppUserId value.
		/// </summary>
		public long AppUserId
		{ get; set; }

		#endregion
	}
}
