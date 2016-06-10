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

	using Microsoft.AspNetCore.Mvc;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Web.ViewModels.Product;
	using WebEngine.Core.Filters;

	#endregion

	/// <summary>
	/// <see cref="ProductController"/> class.
	/// </summary>
	public class ProductController : Controller
	{
		#region Private fields

		private readonly IProductRepository _productRepository;

		private readonly ICategotyRepository _categoryRepository;

		private readonly IProductFilterRepository _productFilterRepository;

		#endregion

		#region Constructors

		public ProductController(
			IProductRepository productRepository,
			ICategotyRepository categoryRepository,
			IProductFilterRepository productFilterRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_productFilterRepository = productFilterRepository;
		}

		#endregion

		[HttpPost]
		public async Task<IActionResult> Index([FromBody]Filter filter)
		{
			if (filter != null)
			{
				


				//IEnumerable<Product> products = await _productRepository.GetProductsAsync(productFilter);

				//if (products != null)
				//{
					
				//}

				return PartialView();
			}

			return View("Error");
		}


		[HttpGet, Route("items/{category}")]
		public async Task<IActionResult> Index(string category)
		{
			if (!string.IsNullOrEmpty(category))
			{
				Category dbCategory = await _categoryRepository.GetCategoryAsync(category);

				if (dbCategory != null)
				{
					ProductsPage page = new ProductsPage();

					page.CategoryName = dbCategory.Name;
					page.CategoryViewName = dbCategory.ViewName;
					page.CurrentPage = 1;

					IEnumerable<Product> products = await _productRepository.GetProductsAsync(dbCategory.Id, page.CurrentPage, 30);

					IEnumerable<ProductFilterItem> filterItems = await _productFilterRepository.GetProductFilterItems(dbCategory.Id);

					if (products != null)
					{
						page.Products = products.Select(p => new ProductView()
						{
							Id = p.Id,
							Name = p.Name,
							ShortInfo = p.ShortInfo,
							CompanyName = p.Company.Name,
							UrlName = p.UrlName
						});
					}

					if (filterItems != null)
					{
						page.FilterItems = filterItems.Select(i => new FilterItemView()
						{
							PropertyId = i.PropertyId,
							FilterItemType = i.FilterItemType,
							PropertyName = i.Property.Name,
							FilterItemValues = i.ProductFilterItemValues.Select(v => v.Value)
						});
					}

					return View(page);
				}
			}

			return View("Error");
		}

		[HttpGet, Route("item/{productId:int}")]
		public async Task<IActionResult> GetProduct(int productId = 0)
		{
			if (productId > 0)
			{
				Product product = await _productRepository.GetProductAsync(productId);

				if (product != null)
				{
					FullProductView productView = GetFullProductView(product);

					return View("Product", productView);
				}
			}

			return View("Error");
		}

		[HttpGet, Route("items/{category}/{urlName}")]
		public async Task<IActionResult> GetProduct(string category, string urlName)
		{
			if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(urlName))
			{
				Product product = await _productRepository.GetProductAsync(category, urlName);

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
			productView.ProductName = product.Name;
			productView.CompanyName = product.Company.Name;

			productView.Properties = product.ProductToProperty
				.Select(s => new ProductPropertyView()
				{
					PropertyId = s.PropertyId,
					PropertyName = s.Property.Name,
					BasePropertyId = s.Property.BaseProperty.Id,
					Value = s.Value,
					SizeValue = s.SizeValue
				}).ToArray();

			return productView;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_productRepository.Dispose();
				_categoryRepository.Dispose();
				_productFilterRepository.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}
