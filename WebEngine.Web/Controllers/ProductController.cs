﻿// -----------------------------------------------------------------------
// <copyright file="SmartPhoneController.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.Controllers
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNet.Mvc;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Web.ViewModels.Product;

	#endregion

	/// <summary>
	/// <see cref="ProductController"/> class.
	/// </summary>
	public class ProductController : Controller
	{
		#region Private fields

		private readonly IProductRepository _smartPhoneRepository;

		#endregion

		#region Constructors

		public ProductController(IProductRepository smartPhoneRepository)
		{
			_smartPhoneRepository = smartPhoneRepository;
		}

		#endregion

		public async Task<IActionResult> Index()
		{
			ProductListView list = new ProductListView();

			IList<Product> products = await _smartPhoneRepository.GetProducts();

			if (products != null)
			{
				list.Products = products.Select(s => new ProductView()
				{
					Id = s.Id,
					Name = s.Name
				})
				.ToArray();
			}

			return View(list);
		}

		[HttpGet, Route("[controller]{productId:int}")]
		public async Task<IActionResult> GetProduct(int productId = 0)
		{
			ProductFullView productView = new ProductFullView();

			Product product = await _smartPhoneRepository.GetProduct(productId);

			if (product != null)
			{
				productView.ProductId = product.Id;
				productView.Name = product.Name;

				productView.Properties = product.ProductToProperty
					.Select(s => new ProductPropertyView()
					{
						PropertyId = s.ProductPropertyId,
						PropertyName = s.Product.Name,
						BasePropertyId = s.ProductProperty.ProductBaseProperty.Id,
						Value = s.Value,
						SizeValue = s.SizeValue
					}).ToArray();

				return View("Product", productView);
			}

			return View("Error");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_smartPhoneRepository.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}
