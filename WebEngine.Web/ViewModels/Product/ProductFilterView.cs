// -----------------------------------------------------------------------
// <copyright file="FilterView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	using Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="ProductFilterView"/> class.
	/// </summary>
	public class ProductFilterView
	{
		/// <summary>
		/// Gets or sets product filter items.
		/// </summary>
		public IList<ProductFilterItem> Items { get; set; }
	}
}
