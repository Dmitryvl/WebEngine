// -----------------------------------------------------------------------
// <copyright file="FilterItem.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductFilterItem"/> class.
	/// </summary>
	public class ProductFilterItem
	{
		#region Public properties

		public int Id { get; set; }

		public int CategoryId { get; set; }

		public int PropertyId { get; set; }

		public string FilterItemType { get; set; }

		#endregion

		#region Navigation properties

		public virtual Category Category { get; set; }

		public virtual Property Property { get; set; }

		public virtual ICollection<ProductFilterItemValue> ProductFilterItemValues { get; set; }

		#endregion
	}
}
