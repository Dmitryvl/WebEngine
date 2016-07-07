// -----------------------------------------------------------------------
// <copyright file="OfferComponent.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewComponents
{

	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Core.Interfaces;
	using Microsoft.AspNetCore.Mvc;
	using Core.Entities;
	using ViewModels.ProductOffer;

	#endregion

	/// <summary>
	/// <see cref="OfferComponent"/> class.
	/// </summary>
	public class OfferComponent : ViewComponent
	{
		private readonly IProductOfferRepository _productOfferRepository;

		public OfferComponent(IProductOfferRepository productOfferRepository)
		{
			_productOfferRepository = productOfferRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync(int productId)
		{
			if (productId > 0)
			{
				IEnumerable<ProductOffer> offers = await _productOfferRepository.GetProductOffersAsync(productId);

				if (offers != null)
				{
					IEnumerable<ProductOfferView> viewModel = offers.Select(o => new ProductOfferView()
					{
						Id = o.Id,
						ProductId = o.ProductId,
						StoreId = o.StoreId,
						StoreName = o.Store.Name,
						Message = o.Message
					});

					return View(viewModel);
				}
			}

			_productOfferRepository.Dispose();

			return View("Error");
		}
	}
}
