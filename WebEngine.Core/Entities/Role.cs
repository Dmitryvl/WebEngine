﻿// -----------------------------------------------------------------------
// <copyright file="Role.cs" author="Dzmitry Prakapenka">
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
	/// <see cref="Role"/> class.
	/// </summary>
	public class Role
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}
