// -----------------------------------------------------------------------
// <copyright file="ProductBaseProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProductBaseProperty"/> class.
	/// </summary>
	public class ProductBaseProperty
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
		/// Gets or sets a value indicating whether <see cref="ProductBaseProperty"/> is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		public virtual ICollection<ProductProperty> ProductProperties { get; set; }

		#endregion
	}
}
