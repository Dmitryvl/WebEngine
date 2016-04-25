// -----------------------------------------------------------------------
// <copyright file="ProductProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductProperty"/> class.
	/// </summary>
	public class ProductProperty
	{
		#region Properties

		/// <summary>
		/// Gets oe sets identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductBasePropertyId"/>.
		/// </summary>
		public int ProductBasePropertyId { get; set; }

		/// <summary>
		/// Gets or sets data type id.
		/// </summary>
		public int DataTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether <see cref="ProductProperty"/> is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets smartphone base property.
		/// </summary>
		public virtual ProductBaseProperty ProductBaseProperty { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductToProperty"/>.
		/// </summary>
		public virtual ICollection<ProductToProperty> ProductToProperty { get; set; }

		/// <summary>
		/// Gets or sets  datatype.
		/// </summary>
		public virtual DataType DataType { get; set; }

		#endregion
	}
}
