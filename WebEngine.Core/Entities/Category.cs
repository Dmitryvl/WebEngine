// -----------------------------------------------------------------------
// <copyright file="Category.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Category"/> class.
	/// </summary>
	public class Category
	{
		#region Properties

		/// <summary>
		/// Gets or sets category id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets category name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets view name.
		/// </summary>
		public string ViewName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether category is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets products.
		/// </summary>
		public virtual ICollection<Product> Products { get; set; }

		#endregion
	}
}
