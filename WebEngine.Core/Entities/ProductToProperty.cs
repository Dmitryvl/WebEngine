// -----------------------------------------------------------------------
// <copyright file="ProductToProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="ProductToProperty"/> class.
	/// </summary>
	public class ProductToProperty
	{
		#region Properties

		/// <summary>
		/// Gets or sets <see cref="ProductId"/>.
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductPropertyId"/>.
		/// </summary>
		public int ProductPropertyId { get; set; }

		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets size value.
		/// </summary>
		public string SizeValue { get; set; }
		
		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets smartphone.
		/// </summary>
		public virtual Product Product { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductProperty"/>.
		/// </summary>
		public virtual ProductProperty ProductProperty { get; set; }

		#endregion
	}
}
