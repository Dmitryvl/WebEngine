﻿// -----------------------------------------------------------------------
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
		#region Properties

		/// <summary>
		/// Gets or sets region identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets region name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets oe sets a value indicating whether region is deleted.
		/// </summary>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets country identifier.
		/// </summary>
		public int CountryId { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets country.
		/// </summary>
		public virtual Country Country { get; set; }

		/// <summary>
		/// Gets or sets cities.
		/// </summary>
		public virtual ICollection<City> Cities { get; set; }

		#endregion
	}
}
