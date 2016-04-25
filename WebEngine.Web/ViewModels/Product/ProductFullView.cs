﻿// -----------------------------------------------------------------------
// <copyright file="ProductFullView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ProductFullView
	{
		public int ProductId { get; set; }

		public string Name { get; set; }

		public IList<ProductPropertyView> Properties { get; set; }
	}
}