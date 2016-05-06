// -----------------------------------------------------------------------
// <copyright file="FilterView.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewComponents
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNet.Mvc;

	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="FilterView"/> 
	/// </summary>
	public class FilterView : ViewComponent
	{
		private ICategotyRepository _categoryRepository;

		public FilterView(ICategotyRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke(string category)
		{
			bool isExist = Task.Run(() => _categoryRepository.IsExist(category)).Result;

			_categoryRepository.Dispose();

			if (isExist)
			{
				return View($"{category.ToLower()}Filter");
			}

			return View("Error");
		}
	}
}
