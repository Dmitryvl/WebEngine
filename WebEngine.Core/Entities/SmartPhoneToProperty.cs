// -----------------------------------------------------------------------
// <copyright file="SmartPhoneToProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="SmartPhoneToProperty"/> class.
	/// </summary>
	public class SmartPhoneToProperty
	{
		#region MyRegion

		/// <summary>
		/// Gets or sets <see cref="SmartPhoneId"/>.
		/// </summary>
		public int SmartPhoneId { get; set; }

		/// <summary>
		/// Gets or sets <see cref="SmartPhonePropertyId"/>.
		/// </summary>
		public int SmartPhonePropertyId { get; set; }

		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets size value.
		/// </summary>
		public string SizeValue { get; set; }
		
		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets smartphone.
		/// </summary>
		public virtual SmartPhone SmartPhone { get; set; }

		/// <summary>
		/// Gets or sets <see cref="SmartPhoneProperty"/>.
		/// </summary>
		public virtual SmartPhoneProperty SmartPhoneProperty { get; set; }

		#endregion
	}
}
