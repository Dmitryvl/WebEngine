// -----------------------------------------------------------------------
// <copyright file="FullProductView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="FullProductView"/> class.
	/// </summary>
	public class FullProductView
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public IList<ProductPropertyView> Properties { get; set; }
	}
}
