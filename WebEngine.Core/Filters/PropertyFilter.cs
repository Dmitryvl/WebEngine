﻿// -----------------------------------------------------------------------
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
		public int PropertyId { get; set; }

		public string Value { get; set; }

		public string SizeValue { get; set; }

		public bool IsRange { get; set; }

		public int RangeId { get; set; }

		public char Operation { get; set; }
	}
}