// -----------------------------------------------------------------------
// <copyright file="ProductFilterItemValue.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="ProductFilterItemValue"/> class.
	/// </summary>
	public class ProductFilterItemValue
	{
		#region Public properties

		/// <summary>
		/// Gets or sets id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets product filter item id.
		/// </summary>
		public int ProductFilterItemId { get; set; }

		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public string Value { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets product filter item.
		/// </summary>
		public virtual ProductFilterItem ProductFilterItem { get; set; }

		#endregion
	}
}
