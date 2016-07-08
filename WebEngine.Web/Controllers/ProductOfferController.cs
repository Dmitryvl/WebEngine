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
	using Microsoft.AspNetCore.Authorization;

	#endregion

	/// <summary>
	/// <see cref="ProductOfferController"/> controller.
	/// </summary>
	//[Route("offers")]
	public class ProductOfferController : Controller
	{
		private readonly IProductOfferRepository _productOfferRepository;

		private readonly IStoreRepository _storeRepository;

		public ProductOfferController(
			IProductOfferRepository productOfferRepository,
			IStoreRepository storeRepository)
		{
			_productOfferRepository = productOfferRepository;
			_storeRepository = storeRepository;
		}

		[Authorize, HttpGet]
		public async Task<IActionResult> SaveOffer(int productId)
		{
			if (productId > 0)
			{
				Store store = await _storeRepository
					.GetStoreForUser(HttpContext.User.Identity.Name);

				if (store != null)
				{
					SaveOfferView offer = new SaveOfferView();
					offer.ProductId = productId;
					offer.StoreId = store.Id;

					return View("SaveOffer", offer);
				}
			}

			return View("Error");
		}

		[Authorize, HttpPost]
		public async Task<IActionResult> SaveOffer([FromForm] SaveOfferView offer)
		{
			if (offer != null)
			{
				ProductOffer dbOffer = new ProductOffer();
				dbOffer.ProductId = offer.ProductId;
				dbOffer.IsActive = true;
				dbOffer.Message = offer.Message;
				dbOffer.StoreId = offer.StoreId;
				dbOffer.Date = DateTime.UtcNow;

				bool success = await _productOfferRepository.SaveProductOfferAsync(dbOffer);

				if (success)
				{
					return RedirectToAction("Index","Home");
				}

				return View("SaveOffer", offer);
			}

			return View("Error");
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

					return View("GetProductOffers", viewModel);
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
