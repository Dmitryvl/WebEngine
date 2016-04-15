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
		public int UserId { get; set; }

		public string UserName { get; set; }

		public int RoleId { get; set; }

		public string RoleName { get; set; }

		public DateTimeOffset RegisterDate { get; set; }

		public IEnumerable<UserStoreView> Stores { get; set; }
	}
}
