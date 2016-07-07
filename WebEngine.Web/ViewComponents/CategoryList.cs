// -----------------------------------------------------------------------
// <copyright file="CategoryList.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewComponents
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Web.ViewModels.Category;

	#endregion

	/// <summary>
	/// <see cref="CategoryList"/> view component.
	/// </summary>
	public class CategoryList : ViewComponent
	{
		private readonly ICategotyRepository _categoryRepository;

		public CategoryList(ICategotyRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			IList<Category> categories = await _categoryRepository.GetCategoriesAsync();

			if (categories != null)
			{
				CategoryView[] cv = categories.Select(c => new CategoryView()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToArray();

				return View(cv);
			}

			_categoryRepository.Dispose();

			return View("Error");
		}
	}
}
