﻿// -----------------------------------------------------------------------
// <copyright file="ProductView.cs" author="Dzmitry Prakapenka">
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
	public class ProductView
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string CompanyName { get; set; }

		public IEnumerable<ProductPropertyView> Properties { get; set; }
	}
}
