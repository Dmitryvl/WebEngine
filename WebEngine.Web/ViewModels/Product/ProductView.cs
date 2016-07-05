// -----------------------------------------------------------------------
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
	/// <see cref="ProductView"/> class.
	/// </summary>
	public class ProductView
	{
		/// <summary>
		/// Gets or sets product id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets product name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets short info about product.
		/// </summary>
		public string ShortInfo { get; set; }

		/// <summary>
		/// Gets or sets url name.
		/// </summary>
		public string UrlName { get; set; }
	}
}
