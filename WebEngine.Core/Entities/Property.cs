// -----------------------------------------------------------------------
// <copyright file="Property.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Property"/> class.
	/// </summary>
	public class Property
	{
		#region Properties

		/// <summary>
		/// Gets or sets identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets <see cref="BasePropertyId"/>.
		/// </summary>
		public int BasePropertyId { get; set; }

		/// <summary>
		/// Gets or sets data type id.
		/// </summary>
		public int DataTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether <see cref="Property"/> is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets smartphone base property.
		/// </summary>
		public virtual BaseProperty BaseProperty { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductToProperty"/>.
		/// </summary>
		public virtual ICollection<ProductToProperty> ProductToProperty { get; set; }

		/// <summary>
		/// Gets or sets product filter items.
		/// </summary>
		public virtual ICollection<ProductFilterItem> ProductFilterItems { get; set; }

		/// <summary>
		/// Gets or sets data type.
		/// </summary>
		public virtual DataType DataType { get; set; }

		#endregion
	}
}
