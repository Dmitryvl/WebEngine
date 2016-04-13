// -----------------------------------------------------------------------
// <copyright file="Country.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Country"/> class.
	/// </summary>
	public class Country
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public virtual ICollection<Region> Regions { get; set; }

	}
}
