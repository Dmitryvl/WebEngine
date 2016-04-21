// -----------------------------------------------------------------------
// <copyright file="SmartPhoneProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="SmartPhoneProperty"/> class.
	/// </summary>
	public class SmartPhoneProperty
	{
		#region Properties

		/// <summary>
		/// Gets oe sets identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets <see cref="SmartPhoneBasePropertyId"/>.
		/// </summary>
		public int SmartPhoneBasePropertyId { get; set; }

		/// <summary>
		/// Gets or sets data type id.
		/// </summary>
		public int DataTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether <see cref="SmartPhoneProperty"/> is active.
		/// </summary>
		public bool IsActive { get; set; }

		#endregion

		#region Navigation properties

		/// <summary>
		/// Gets or sets smartphone base property.
		/// </summary>
		public virtual SmartPhoneBaseProperty SmartPhoneBaseProperty { get; set; }

		/// <summary>
		/// Gets or sets <see cref="SmartPhoneToProperty"/>.
		/// </summary>
		public virtual ICollection<SmartPhoneToProperty> SmartPhoneToProperty { get; set; }

		/// <summary>
		/// Gets or sets  datatype.
		/// </summary>
		public virtual DataType DataType { get; set; }

		#endregion
	}
}
