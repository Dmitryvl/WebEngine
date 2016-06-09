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

		/// <summary>
		/// Gets or sets id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets category id.
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Gets or sets property id.
		/// </summary>
		public int PropertyId { get; set; }

		/// <summary>
		/// Gets or sets filter item type.
		/// </summary>
		public string FilterItemType { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets category.
		/// </summary>
		public virtual Category Category { get; set; }

		/// <summary>
		/// Gets or sets property.
		/// </summary>
		public virtual Property Property { get; set; }

		/// <summary>
		/// Gets or sets product filter item values.
		/// </summary>
		public virtual ICollection<ProductFilterItemValue> ProductFilterItemValues { get; set; }

		#endregion
	}
}
