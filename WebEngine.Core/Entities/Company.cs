// -----------------------------------------------------------------------
// <copyright file="Company.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="Company"/> class.
	/// </summary>
	public class Company
	{
		/// <summary>
		/// Gets or sets company identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets company name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether company is active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets smartphones.
		/// </summary>
		public virtual ICollection<SmartPhone> SmartPhones { get; set; }
	}
}
