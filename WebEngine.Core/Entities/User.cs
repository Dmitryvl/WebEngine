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
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// <see cref="User"/> class.
	/// </summary>
	public class User
	{
		#region Properties

		public int Id { get; set; }

		public string Name { get; set; }

		public int RoleId { get; set; }

		public string Password { get; set; }

		public string PasswordSalt { get; set; }

		public string Email { get; set; }

		public Guid? EmailKey { get; set; }

		public DateTime RegisterDate { get; set; }

		public bool IsActive { get; set; }

		public bool IsDeleted { get; set; }

		#endregion

		#region Navigation properties

		public virtual Role Role { get; set; } 
		
		public virtual ICollection<Store> Stores { get; set; }

		#endregion
	}
}
