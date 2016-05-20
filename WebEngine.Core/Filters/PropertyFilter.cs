// -----------------------------------------------------------------------
// <copyright file="PropertyFilter.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Filters
{
	#region Usings

	#endregion

	/// <summary>
	/// <see cref="PropertyFilter"/> class.
	/// </summary>
	public class PropertyFilter
	{
		/// <summary>
		/// Gets or sets property id.
		/// </summary>
		public int PropertyId { get; set; }

		/// <summary>
		/// Gets or sets property value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets property size value.
		/// </summary>
		public string SizeValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether property is range property.
		/// </summary>
		public bool IsRange { get; set; }

		/// <summary>
		/// Gets or sets range id.
		/// </summary>
		public int RangeId { get; set; }

		/// <summary>
		/// Gets or sets operation.
		/// </summary>
		public char Operation { get; set; }
	}
}
