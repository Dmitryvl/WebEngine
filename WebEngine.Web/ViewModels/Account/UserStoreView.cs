﻿// -----------------------------------------------------------------------
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
		public int StoreId { get; set; }

		public string StoreName { get; set; }

		public DateTimeOffset CreationDate { get; set; }

		public bool IsActive { get; set; }
	}
}