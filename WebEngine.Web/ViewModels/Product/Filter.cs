// -----------------------------------------------------------------------
// <copyright file="Filter.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Product
{
	#region Usings
	
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	
	#endregion 

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class Filter
	{
		public string Category { get; set; }

		public IList<FilterProperty> Properties { get; set; }
	}
}