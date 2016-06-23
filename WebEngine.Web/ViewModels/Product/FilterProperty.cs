// -----------------------------------------------------------------------
// <copyright file="FilterProperty.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings


	#endregion

	/// <summary>
	/// <see cref="FilterProperty"/> class.
	/// </summary>
	public class FilterProperty
	{
		/// <summary>
		/// Gets or sets id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets a value idicating whether property is range.
		/// </summary>
		public bool IsRange { get; set; }

		/// <summary>
		/// Gets or sets operator.
		/// </summary>
		public char Operator { get; set; }
	}
}