// -----------------------------------------------------------------------
// <copyright file="User.cs" author="Dzmitry Prakapenka">
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
	/// <see cref="User"/> class.
	/// </summary>
	public class User
	{
		#region Properties

		/// <summary>
		/// Gets or sets user identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets user name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets role identifier.
		/// </summary>
		public int RoleId { get; set; }

		/// <summary>
		/// Gets or sets password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets password salt.
		/// </summary>
		public string PasswordSalt { get; set; }

		/// <summary>
		/// Gets or sets email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets email key.
		/// </summary>
		public Guid? EmailKey { get; set; }

		/// <summary>
		/// Gets or sets register date.
		/// </summary>
		public DateTimeOffset RegisterDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether user is active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether user is deleted.
		/// </summary>
		public bool IsDeleted { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets role.
		/// </summary>
		public virtual Role Role { get; set; } 
		
		/// <summary>
		/// Gets or sets stores.
		/// </summary>
		public virtual ICollection<Store> Stores { get; set; }

		#endregion
	}
}
