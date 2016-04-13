// -----------------------------------------------------------------------
// <copyright file="Region.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Region"/> class.
	/// </summary>
	public class Region
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public int CountryId { get; set; }

		public virtual Country Country { get; set; }

		public virtual ICollection<City> Cities { get; set; }
	}
}
