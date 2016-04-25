// -----------------------------------------------------------------------
// <copyright file="ProductListView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="ProductListView"/> class.
	/// </summary>
	public class ProductListView
	{
		public IList<ProductView> Products { get; set; }
	}
}
