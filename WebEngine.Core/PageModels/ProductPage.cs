// -----------------------------------------------------------------------
// <copyright file="ProductPage.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.PageModels
{
	#region Usings

	using System.Collections.Generic;
	using Entities;

	#endregion

	/// <summary>
	/// <see cref="ProductPage"/> class.
	/// </summary>
	public class ProductPage
	{
		/// <summary>
		/// Gets or sets products.
		/// </summary>
		public IList<Product> Products { get; set; }

		/// <summary>
		/// Gets or sets total pages.
		/// </summary>
		public int TotalPages { get; set; }

		/// <summary>
		/// Gets or sets category name.
		/// </summary>
		public string CategoryName { get; set; }
	}
}
