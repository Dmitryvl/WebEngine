// -----------------------------------------------------------------------
// <copyright file="ProductOffer.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace WebEngine.Core.Entities
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="ProductOffer"/> class.
	/// </summary>
	public class ProductOffer
	{
		#region Properties

		/// <summary>
		/// Gets or sets id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets store identifier.
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// Gets or sets smartphone identifier.
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Gets or sets message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether offer is active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Offer date.
		/// </summary>
		public DateTimeOffset Date { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets store.
		/// </summary>
		public virtual Store Store { get; set; }

		/// <summary>
		/// Gets or sets smartphone.
		/// </summary>
		public virtual Product Product { get; set; }
		
		#endregion
	}
}
