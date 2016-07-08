// <copyright file="StoreView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Store
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// <see cref="StoreView"/> class.
	/// </summary>
	public class StoreView
	{
		public int StoreId { get; set; }

		public string StoreName { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
