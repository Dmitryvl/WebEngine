// -----------------------------------------------------------------------
// <copyright file="SmartPhone.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// <see cref="SmartPhone"/> class.
	/// </summary>
	public class SmartPhone
	{
		/// <summary>
		/// Gets or sets smartphone identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets companty identifier.
		/// </summary>
		public int CompanyId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether smartphone is active.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets company.
		/// </summary>
		public virtual Company Company { get; set; }
	}
}
