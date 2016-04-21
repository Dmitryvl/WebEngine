// -----------------------------------------------------------------------
// <copyright file="DataType.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Entities
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// <see cref="DataType"/> class.
	/// </summary>
	public class DataType
	{
		#region MyRegion

		/// <summary>
		/// Gets or sets id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name.
		/// </summary>
		public string Name { get; set; }

		#endregion

		#region MyRegion

		/// <summary>
		/// Gets or sets <see cref="SmartPhoneProperties"/>
		/// </summary>
		public virtual ICollection<SmartPhoneProperty> SmartPhoneProperties { get; set; }
		
		#endregion
	}
}
