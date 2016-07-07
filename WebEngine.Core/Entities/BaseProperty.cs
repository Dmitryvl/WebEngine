// -----------------------------------------------------------------------
// <copyright file="BaseProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="BaseProperty"/> class.
	/// </summary>
	public class BaseProperty
	{
		#region Properties

		/// <summary>
		/// Gets or sets identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether <see cref="BaseProperty"/> is active.
		/// </summary>
		public bool IsActive { get; set; }

		public int CategoryId { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets properties.
		/// </summary>
		public virtual ICollection<Property> Properties { get; set; }

		public virtual Category Category { get; set; }

		#endregion
	}
}
