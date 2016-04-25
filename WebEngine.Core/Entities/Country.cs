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
		#region Properties

		/// <summary>
		/// Gets or sets country identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets country name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether country is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets regions.
		/// </summary>
		public virtual ICollection<Region> Regions { get; set; }

		#endregion

	}
}
