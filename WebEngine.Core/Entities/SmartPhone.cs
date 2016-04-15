// -----------------------------------------------------------------------
// <copyright file="SmartPhone.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="SmartPhone"/> class.
	/// </summary>
	public class SmartPhone
	{
		#region Properties

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
		/// Gets or sets smartphone processor id.
		/// </summary>
		public int SmartPhoneProcessorId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether smartphone is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets company.
		/// </summary>
		public virtual Company Company { get; set; }

		/// <summary>
		/// Gets or sets smartphone processor.
		/// </summary>
		public virtual SmartPhoneProcessor SmartPhoneProcessor { get; set; }

		/// <summary>
		/// Gets or sets <see cref="SmartPhoneOffer"/>.
		/// </summary>
		public virtual ICollection<SmartPhoneOffer> SmartPhoneOffer { get; set; }
		
		#endregion
	}
}
