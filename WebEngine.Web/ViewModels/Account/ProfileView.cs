// -----------------------------------------------------------------------
// <copyright file="Profile.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Account
{

	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="ProfileView"/> class.
	/// </summary>
	public class ProfileView
	{
		/// <summary>
		/// Gets or sets user id.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets user name.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets role id.
		/// </summary>
		public int RoleId { get; set; }

		/// <summary>
		/// Gets or sets role name.
		/// </summary>
		public string RoleName { get; set; }

		/// <summary>
		/// Gets or sets register date.
		/// </summary>
		public DateTimeOffset RegisterDate { get; set; }

		/// <summary>
		/// Gets or sets user stores.
		/// </summary>
		public IEnumerable<UserStoreView> Stores { get; set; }
	}
}
