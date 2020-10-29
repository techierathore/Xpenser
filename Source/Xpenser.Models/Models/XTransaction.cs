using System;
using System.Data;
using System.Data.Common;

namespace Xpenser.Models
{
	public class XTransaction
	{
		#region Properties
		/// <summary>
		/// Gets or sets the TransactionId value.
		/// </summary>
		public long TransactionId
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
		/// Gets or sets the AppUserId value.
		/// </summary>
		public long AppUserId
		{ get; set; }

		/// <summary>
		/// Gets or sets the CategoryId value.
		/// </summary>
		public long CategoryId
		{ get; set; }

		/// <summary>
		/// Gets or sets the AccountId value.
		/// </summary>
		public long AccountId
		{ get; set; }

		/// <summary>
		/// Gets or sets the ReeciptPath value.
		/// </summary>
		public string ReeciptPath
		{ get; set; }

		#endregion
	}
}
