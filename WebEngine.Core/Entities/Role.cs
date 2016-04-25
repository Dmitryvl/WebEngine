// -----------------------------------------------------------------------
// <copyright file="Role.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Role"/> class.
	/// </summary>
	public class Role
	{
		#region Properties

		/// <summary>
		/// Gets or sets role identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets role name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether role is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets users.
		/// </summary>
		public virtual ICollection<User> Users { get; set; }

		#endregion
	}
}
