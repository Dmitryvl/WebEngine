// -----------------------------------------------------------------------
// <copyright file="ProductFilterItemValue.cs" author="Dzmitry Prakapenka">
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
	/// <see cref="ProductFilterItemValue"/> class.
	/// </summary>
	public class ProductFilterItemValue
	{
		#region Public properties

		public int Id { get; set; }

		public int ProductFilterItemId { get; set; }

		public string Value { get; set; }

		#endregion

		#region Navigation properties

		public virtual ProductFilterItem ProductFilterItem { get; set; }

		#endregion
	}
}
