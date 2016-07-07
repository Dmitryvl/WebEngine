// -----------------------------------------------------------------------
// <copyright file="ProductOfferRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{

	#region Usings

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="ProductOfferRepository"/> class.
	/// </summary>
	public class ProductOfferRepository : BaseRepository<ProductOfferRepository>, IProductOfferRepository
	{
		public ProductOfferRepository(IServiceProvider services) : base(services)
		{
		}

		public async Task<IList<ProductOffer>> GetProductOffers(int productId)
		{
			if (productId > DEFAULT_ID)
			{
				try
				{
					IList<ProductOffer> offers = await _context.ProductOffers
						.Where(o => o.IsActive == true)
						.Select(o => new ProductOffer()
						{
							ProductId = o.ProductId,
							StoreId = o.StoreId,
							Message = o.Message,
							IsActive = o.IsActive
						})
						.ToArrayAsync()
						.ConfigureAwait(false);

					return offers;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return null;
				}
			}

			_logger.LogWarning("GetProductOffers: productId <= 0");

			return null;
		}

		public async Task<bool> InactiveProductOffer(int productOfferId)
		{
			if (productOfferId > DEFAULT_ID)
			{
				try
				{
					ProductOffer offer = await _context.ProductOffers
						.FirstOrDefaultAsync(o => o.Id == productOfferId)
						.ConfigureAwait(false);

					if (offer != null)
					{
						offer.IsActive = false;

						_context.Entry(offer).State = EntityState.Modified;

						await _context.SaveChangesAsync().ConfigureAwait(false);

						return true;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return false;
				}
			}

			_logger.LogWarning("InactiveProductOffer: productOfferId <= 0");

			return false;
		}

		public async Task<bool> SaveProductOffer(ProductOffer productOffer)
		{
			if (productOffer != null)
			{
				try
				{
					if (productOffer.Id > DEFAULT_ID)
					{
						ProductOffer offer = await _context.ProductOffers
							.FirstOrDefaultAsync(o => o.Id == productOffer.Id)
							.ConfigureAwait(false);

						if (offer != null)
						{
							offer.Message = offer.Message;
							offer.Date = DateTimeOffset.Now;

							_context.Entry(offer).State = EntityState.Modified;

							await _context.SaveChangesAsync().ConfigureAwait(false);

							return true;
						}
					}
					else
					{
						_context.ProductOffers.Add(productOffer);

						await _context.SaveChangesAsync().ConfigureAwait(false);

						return true;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return false;
				}
			}

			_logger.LogWarning("SaveProductOffer: productOffer is null!");

			return false;
		}
	}
}
