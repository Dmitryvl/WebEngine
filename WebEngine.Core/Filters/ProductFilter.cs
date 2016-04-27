// -----------------------------------------------------------------------
// <copyright file="ProductFilter.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Filters
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductFilter"/> class.
	/// </summary>
	public class ProductFilter
	{
		public string CategoryName { get; set; }

		public IList<PropertyFilter> Properties { get; set;}
	}
}
