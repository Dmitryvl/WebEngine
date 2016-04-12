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
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	
	#endregion 

	/// <summary>
	/// <see cref="Store"/> class.
	/// </summary>
	public class Store
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Name { get; set; }

		public DateTimeOffset CreationDate { get; set; }

		public bool IsActive { get; set; }

		public virtual User User { get; set; }
	}
}
