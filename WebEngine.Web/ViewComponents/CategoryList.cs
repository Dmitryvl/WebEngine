// -----------------------------------------------------------------------
// <copyright file="CategoryList.cs" company="EPAM Systems">
//     Copyright (c) "EPAM Systems". All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewComponents
{

	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Mvc;
	using Core.Interfaces;
	using Core.Entities;
	using ViewModels.Category;
	#endregion

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class CategoryList : ViewComponent
	{
		private ICategotyRepository _categoryRepository;

		public CategoryList(ICategotyRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke()
		{
			IList<Category> categories = Task.Run(() => _categoryRepository.GetCategories()).Result;

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
