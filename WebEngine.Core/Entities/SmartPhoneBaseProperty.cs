// -----------------------------------------------------------------------
// <copyright file="SmartPhoneBaseProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="SmartPhoneBaseProperty"/> class.
	/// </summary>
	public class SmartPhoneBaseProperty
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
		/// Gets or sets a value indicating whether <see cref="SmartPhoneBaseProperty"/> is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		public virtual ICollection<SmartPhoneProperty> SmartPhoneProperties { get; set; }

		#endregion
	}
}
