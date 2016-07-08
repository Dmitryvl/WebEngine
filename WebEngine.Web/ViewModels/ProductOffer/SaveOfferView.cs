// -----------------------------------------------------------------------
// <copyright file="SaveOfferView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.ProductOffer
{
	#region Usings
	
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	
	#endregion

	/// <summary>
	/// <see cref="SaveOfferView"/> class.
	/// </summary>
	public class SaveOfferView
	{
		public int ProductId { get; set; }

		public int StoreId { get; set; }

		public string Message { get; set; }
	}
}
