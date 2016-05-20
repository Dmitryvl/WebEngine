﻿// -----------------------------------------------------------------------
// <copyright file="ProductListView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductListView"/> class.
	/// </summary>
	public class ProductListView
	{
		/// <summary>
		/// Gets or sets category name.
		/// </summary>
		public string CategoryName { get; set; }

		/// <summary>
		/// Gets or sets category view name.
		/// </summary>
		public string CategoryViewName { get; set; }

		/// <summary>
		/// Gets or sets products.
		/// </summary>
		public IEnumerable<ProductView> Products { get; set; }
	}
}
