// -----------------------------------------------------------------------
// <copyright file="SmartPhoneOffer.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="SmartPhoneOffer"/> class.
	/// </summary>
	public class SmartPhoneOffer
	{
		#region Properties

		/// <summary>
		/// Gets or sets store identifier.
		/// </summary>
		public int StoreId { get; set; }

		/// <summary>
		/// Gets or sets smartphone identifier.
		/// </summary>
		public int SmartPhoneId { get; set; }

		/// <summary>
		/// Gets or sets message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether offer is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets store.
		/// </summary>
		public virtual Store Store { get; set; }

		/// <summary>
		/// Gets or sets smartphone.
		/// </summary>
		public virtual SmartPhone SmartPhone { get; set; }
		
		#endregion
	}
}
