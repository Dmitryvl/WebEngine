// -----------------------------------------------------------------------
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
		public string CategoryName { get; set; }

		public IEnumerable<ProductView> Products { get; set; }
	}
}
