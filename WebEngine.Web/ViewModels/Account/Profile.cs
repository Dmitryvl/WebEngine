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
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	
	#endregion 

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class Profile
	{
		public int UserId { get; set; }

		public string UserName { get; set; }

		public int RoleId { get; set; }

		public string RoleName { get; set; }

		public DateTimeOffset RegisterDate { get; set; }
	}
}
