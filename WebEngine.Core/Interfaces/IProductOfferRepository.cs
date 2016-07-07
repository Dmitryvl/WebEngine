// -----------------------------------------------------------------------
// <copyright file="IProductOfferRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{

	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="IProductOfferRepository"/> interface.
	/// </summary>
	public interface IProductOfferRepository : IDisposable
	{
		Task<bool> SaveProductOfferAsync(ProductOffer productOffer);

		Task<IList<ProductOffer>> GetProductOffersAsync(int productId);

		Task<bool> InactiveProductOfferAsync(int productOfferId);
	}
}
