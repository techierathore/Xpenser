using System;
using System.Data;
using System.Data.Common;

namespace Xpenser.Models
{
	public class Category
	{
		#region Properties
		/// <summary>
		/// Gets or sets the CategoryId value.
		/// </summary>
		public long CategoryId
		{ get; set; }

		/// <summary>
		/// Gets or sets the CategoryName value.
		/// </summary>
		public string CategoryName
		{ get; set; }

		/// <summary>
		/// Gets or sets the CategoryDescription value.
		/// </summary>
		public string CategoryDescription
		{ get; set; }

		/// <summary>
		/// Gets or sets the ParentId value.
		/// </summary>
		public long ParentId
		{ get; set; }

		/// <summary>
		/// Gets or sets the IconPath value.
		/// </summary>
		public string IconPath
		{ get; set; }

		#endregion
	}
}
