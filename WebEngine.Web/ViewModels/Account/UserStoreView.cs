// -----------------------------------------------------------------------
// <copyright file="UserStoreView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Account
{
	#region Usings
	
	using System;

	#endregion

	/// <summary>
	/// <see cref="UserStoreView"/> class.
	/// </summary>
	public class UserStoreView
	{
		/// <summary>
		/// Gets or sets store id.
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// Gets or sets store name.
		/// </summary>
		public string StoreName { get; set; }

		/// <summary>
		/// Gets or sets creation date.
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether store is active.
		/// </summary>
		public bool IsActive { get; set; }
	}
}
