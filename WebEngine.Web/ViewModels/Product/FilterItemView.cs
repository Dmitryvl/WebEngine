// -----------------------------------------------------------------------
// <copyright file="FilterItemView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="FilterItemView"/> class.
	/// </summary>
	public class FilterItemView
	{
		public int PropertyId { get; set; }

		public string PropertyName { get; set; }

		public string FilterItemType { get; set; }

		public IEnumerable<string> FilterItemValues { get; set; }
	}
}
