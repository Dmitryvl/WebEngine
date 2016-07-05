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

	using Microsoft.AspNetCore.Mvc;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Web.ViewModels.Product;
	using WebEngine.Core.Filters;
	using Core.PageModels;
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

		private readonly IProductPageRepository _productPageRepository;

		private const int PAGE_SIZE = 30;

		#endregion

		#region Constructors

		public ProductController(
			IProductRepository productRepository,
			ICategotyRepository categoryRepository,
			IProductFilterRepository productFilterRepository,
			IProductPageRepository productPageRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_productFilterRepository = productFilterRepository;
			_productPageRepository = productPageRepository;
		}

		#endregion

		[HttpPost]
		public async Task<IActionResult> Index([FromBody]Filter filter)
		{
			if (filter != null)
			{
				Category category = await _categoryRepository.GetCategoryAsync(filter.CategoryId);

				if (category != null)
				{
					const string any = "any";

					ProductFilter productFilter = new ProductFilter();
					productFilter.CategoryId = filter.CategoryId;
					productFilter.CurrentPage = filter.CurrentPage;
					productFilter.PageSize = PAGE_SIZE;
					productFilter.Properties = filter.Properties
						.Where(p => p.Id > 0 && !string.IsNullOrEmpty(p.Value) && p.Value != any)
						.Select(p => new PropertyFilter()
						{
							PropertyId = p.Id,
							Value = p.Value,
							IsRange = p.IsRange,
							Operation = p.Operator
						})
						.ToArray();

					ProductPage page = await _productPageRepository.GetProductPage(productFilter);

					if (page != null)
					{
						ProductPageView view = new ProductPageView();
						view.CurrentPage = filter.CurrentPage;
						view.TotalPages = page.TotalPages;
						view.CategoryName = category.Name;
						view.Products = page.Products.Select(p => new ProductView()
						{
							Id = p.Id,
							Name = p.Name,
							ShortInfo = p.ShortInfo,
							UrlName = p.UrlName
						});

						return PartialView("_ShortProduct", view);
					}
				}
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
					ProductFilter productFilter = new ProductFilter()
					{
						CategoryId = dbCategory.Id,
						CategoryName = dbCategory.Name,
						CurrentPage = 1,
						PageSize = PAGE_SIZE
					};

					ProductPage productPage = await _productPageRepository.GetProductPage(productFilter);

					if (productPage != null)
					{
						ProductPageView page = new ProductPageView();

						page.CategoryName = dbCategory.Name;
						page.CategoryViewName = dbCategory.ViewName;
						page.CategoryId = dbCategory.Id;
						page.CurrentPage = 1;
						page.TotalPages = productPage.TotalPages;

						IEnumerable<ProductFilterItem> filterItems = await _productFilterRepository.GetProductFilterItems(dbCategory.Id);

						if (productPage.Products != null)
						{
							page.Products = productPage.Products.Select(p => new ProductView()
							{
								Id = p.Id,
								Name = p.Name,
								ShortInfo = p.ShortInfo,
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
