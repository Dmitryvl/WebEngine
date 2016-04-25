// -----------------------------------------------------------------------
// <copyright file="ProductPropertyView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	#endregion

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ProductPropertyView
	{
		public int PropertyId { get; set; }

		public int BasePropertyId { get; set; }

		public string PropertyName { get; set; }

		public string Value { get; set; }

		public string SizeValue { get; set; }
	}
}
