// -----------------------------------------------------------------------
// <copyright file="Filter.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Filter"/> class.
	/// </summary>
	public class Filter
	{
		/// <summary>
		/// Gets or sets filter properties.
		/// </summary>
		public IList<FilterProperty> Properties { get; set; }

		/// <summary>
		/// Gets or sets category.
		/// </summary>
		public string Category { get; set; }

		/// <summary>
		/// Gets or sets current page.
		/// </summary>
		public int CurrentPage { get; set; }
	}
}
