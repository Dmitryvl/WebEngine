// -----------------------------------------------------------------------
// <copyright file="ProductOfferController.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WebEngine.Web.ViewModels.ProductOffer;
	using Core.Interfaces;
	using Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="ProductOfferController"/> controller.
	/// </summary>
	public class ProductOfferController : Controller
	{
		private readonly IProductOfferRepository _productOfferRepository;

		public ProductOfferController(
			IProductOfferRepository productOfferRepository)
		{
			_productOfferRepository = productOfferRepository;
		}


		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> GetProductOffers(int productId)
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

			return View("Error");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_productOfferRepository.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}
