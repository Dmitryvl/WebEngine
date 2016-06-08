// -----------------------------------------------------------------------
// <copyright file="ProductFilter.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Filters
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductFilter"/> class.
	/// </summary>
	public class ProductFilter
	{
		/// <summary>
		/// Gets or sets category name.
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Gets or sets properties.
		/// </summary>
		public IList<PropertyFilter> Properties { get; set; }

		/// <summary>
		/// Gets or sets page size.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Gets or sets current page.
		/// </summary>
		public int CurrentPage { get; set; }
	}
}
