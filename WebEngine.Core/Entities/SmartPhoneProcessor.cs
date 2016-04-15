// -----------------------------------------------------------------------
// <copyright file="SmartPhoneProcessor.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="SmartPhoneProcessor"/> class.
	/// </summary>
	public class SmartPhoneProcessor
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
		/// Gets or sets clock rate.
		/// </summary>
		public float ClockRate { get; set; }

		/// <summary>
		/// Gets or sets count of kernels.
		/// </summary>
		public int KernelsCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether processor is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties
		
		/// <summary>
		/// Gets or sets smartphones.
		/// </summary>
		public virtual ICollection<SmartPhone> SmartPhones { get; set; }

		#endregion
	}
}
