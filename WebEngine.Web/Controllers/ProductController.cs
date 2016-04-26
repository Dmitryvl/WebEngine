// -----------------------------------------------------------------------
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

		[HttpGet, Route("[controller]/{category}")]
		public async Task<IActionResult> Index(string category)
		{
			if (!string.IsNullOrEmpty(category))
			{
				ProductListView list = new ProductListView();

				IList<Product> products = await _smartPhoneRepository.GetProducts(category);

				if (products != null)
				{
					list.Products = products.Select(s => new ProductView()
					{
						Id = s.Id,
						Name = s.Name,
						CompanyName = s.Company.Name
					})
					.ToArray();
				}

				return View(list);
			}

			return View("Error");
		}

		[HttpGet, Route("[controller]{productId:int}")]
		public async Task<IActionResult> GetProduct(int productId = 0)
		{
			if (productId > 0)
			{
				Product product = await _smartPhoneRepository.GetProduct(productId);

				if (product != null)
				{
					FullProductView productView = GetFullProductView(product);

					return View("Product", productView);
				}
			}

			return View("Error");
		}

		[HttpGet, Route("[controller]/{category}/{productId:int}")]
		public async Task<IActionResult> GetProduct(string category, int productId = 0)
		{
			if (!string.IsNullOrEmpty(category) && productId > 0)
			{
				Product product = await _smartPhoneRepository.GetProduct(category, productId);

				if (product != null)
				{
					FullProductView productView = GetFullProductView(product);

					return View("Product", productView);
				}
			}

			return View("Error");
		}

		[HttpGet, Route("[controller]/{category}/{productUrlName}")]
		public async Task<IActionResult> GetProduct(string category, string productUrlName)
		{
			if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(productUrlName))
			{
				Product product = await _smartPhoneRepository.GetProduct(category, productUrlName);

				if (product != null)
				{
					FullProductView productView = GetFullProductView(product);

					return View("Product", productView);
				}
			}

			return View("Error");
		}

		private FullProductView GetFullProductView(Product product)
		{
			FullProductView productView = new FullProductView();

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

			return productView;
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
