// -----------------------------------------------------------------------
// <copyright file="Country.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="City"/> class.
	/// </summary>
	public class City
	{
		#region Properties

		/// <summary>
		/// Gets or sets city identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets city name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether city is active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets region identifier.
		/// </summary>
		public int RegionId { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets region.
		/// </summary>
		public virtual Region Region { get; set; }
		
		#endregion
	}
}
