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
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public int RegionId { get; set; }

		public virtual Region Region { get; set; }
	}
}
