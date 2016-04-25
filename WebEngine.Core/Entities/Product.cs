// -----------------------------------------------------------------------
// <copyright file="Product.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Product"/> class.
	/// </summary>
	public class Product
	{
		#region Properties

		/// <summary>
		/// Gets or sets smartphone identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets url name.
		/// </summary>
		public string UrlName { get; set; }

		/// <summary>
		/// Gets or sets companty identifier.
		/// </summary>
		public int CompanyId { get; set; }

		/// <summary>
		/// Gets or sets category id.
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether smartphone is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets company.
		/// </summary>
		public virtual Company Company { get; set; }

		/// <summary>
		/// Gets or sets category.
		/// </summary>
		public virtual Category Category { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductOffer"/>.
		/// </summary>
		public virtual ICollection<ProductOffer> ProductOffer { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductToProperty"/>.
		/// </summary>
		public virtual ICollection<ProductToProperty> ProductToProperty { get; set; }

		#endregion
	}
}
