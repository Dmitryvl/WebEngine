// -----------------------------------------------------------------------
// <copyright file="Store.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Store"/> class.
	/// </summary>
	public class Store
	{
		#region Properties

		/// <summary>
		/// Gets or sets store identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets user identifier.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets store name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets creation date.
		/// </summary>
		public DateTimeOffset CreationDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether store is deleted.
		/// </summary>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether store is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets owner.
		/// </summary>
		public virtual User User { get; set; }

		/// <summary>
		/// Gets or sets <see cref="OfferSmartPhone"/>.
		/// </summary>
		public virtual ICollection<OfferSmartPhone> OfferSmartPhone { get; set; }

		#endregion
	}
}
