// -----------------------------------------------------------------------
// <copyright file="FilterView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewComponents
{
	#region Usings

	using System.Threading.Tasks;

	using Core.Entities;

	using Microsoft.AspNetCore.Mvc;

	using ViewModels.Product;

	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="FilterView"/> class.
	/// </summary>
	public class FilterView : ViewComponent
	{
		private readonly ICategotyRepository _categoryRepository;

		private readonly IProductFilterRepository _productFilterRepository;

		public FilterView(
			ICategotyRepository categoryRepository,
			IProductFilterRepository productFilterRepository)
		{
			_categoryRepository = categoryRepository;
			_productFilterRepository = productFilterRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync(string category)
		{
			Category dbCategory = await _categoryRepository.GetCategoryAsync(category);

			if (dbCategory != null)
			{
				ProductFilterView filterView = new ProductFilterView();

				filterView.Items = await _productFilterRepository.GetProductFilterItems(dbCategory.Id);

				_categoryRepository.Dispose();
				_productFilterRepository.Dispose();

				return View(filterView);
			}
			else
			{
				_categoryRepository.Dispose();
				_productFilterRepository.Dispose();

				return View("Error");
			}
		}
	}
}
