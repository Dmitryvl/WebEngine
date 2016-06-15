// -----------------------------------------------------------------------
// <copyright file="ProductsPageView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductPageView"/> class.
	/// </summary>
	public class ProductPageView
	{
		/// <summary>
		/// Gets or sets products.
		/// </summary>
		public IEnumerable<ProductView> Products { get; set; }

		/// <summary>
		/// Gets or sets filter items.
		/// </summary>
		public IEnumerable<FilterItemView> FilterItems { get; set; }

		/// <summary>
		/// Gets or sets category name.
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Gets or sets category view name.
		/// </summary>
		public string CategoryViewName { get; set; }

		/// <summary>
		/// Gets or sets category id.
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Gets or sets current page.
		/// </summary>
		public int CurrentPage { get; set; }

		/// <summary>
		/// Gets or sets total pages.
		/// </summary>
		public int TotalPages { get; set; }
	}
}
