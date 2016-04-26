// -----------------------------------------------------------------------
// <copyright file="CategoryController.cs" author="Dzmitry Prakapenka">
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
	using Microsoft.AspNet.Mvc;
	using WebEngine.Core.Interfaces;
	using Core.Entities;
	using ViewModels.Category;
	#endregion

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class CategoryController : Controller
	{
		#region Private fields

		private ICategotyRepository _categoryRepository;

		#endregion

		#region Constructors

		public CategoryController(ICategotyRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		#endregion

		[HttpGet]
		public async Task<PartialViewResult> GetCategories()
		{
			IList<Category> categories = await _categoryRepository.GetCategories();

			if (categories != null)
			{
				CategoryView[] cw = categories.Select(c => new CategoryView()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToArray();
			}

			return PartialView();
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_categoryRepository.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}
