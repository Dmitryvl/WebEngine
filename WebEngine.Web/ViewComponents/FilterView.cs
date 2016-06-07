// -----------------------------------------------------------------------
// <copyright file="FilterView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewComponents
{
	#region Usings

	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="FilterView"/> class.
	/// </summary>
	public class FilterView : ViewComponent
	{
		private ICategotyRepository _categoryRepository;

		public FilterView(ICategotyRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync(string category)
		{
			bool isExist = await _categoryRepository.IsExistAsync(category);

			_categoryRepository.Dispose();

			if (isExist)
			{
				return View($"{category.ToLower()}Filter");
			}

			return View("Error");
		}
	}
}
