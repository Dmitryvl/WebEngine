// -----------------------------------------------------------------------
// <copyright file="Category.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class Category
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsActive { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
