// <copyright file="CategoryRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.Extensions.Options;
	using Microsoft.EntityFrameworkCore;

	using WebEngine.Core.Config;
	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="CategoryRepository"/> class.
	/// </summary>
	public class CategoryRepository : BaseRepository, ICategotyRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public CategoryRepository(IServiceProvider services, IOptions<AppConfig> config) : base(services, config)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get categories.
		/// </summary>
		/// <returns>Return category collection.</returns>
		public async Task<IList<Category>> GetCategories()
		{
			try
			{
				Category[] categories = await _context.Categories.ToArrayAsync();

				return categories;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		/// <summary>
		/// Get category by name.
		/// </summary>
		/// <param name="categoryName">Category name.</param>
		/// <returns>Return category.</returns>
		public async Task<Category> GetCategory(string categoryName)
		{
			if (!string.IsNullOrEmpty(categoryName))
			{
				try
				{
					Category category = await _context.Categories
						.FirstOrDefaultAsync(c => c.Name == categoryName);

					return category;
				}
				catch
				{
					return null;
				}
			}

			return null;
		}

		/// <summary>
		/// Checking category by name.
		/// </summary>
		/// <param name="categoryName">Category name.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> IsExist(string categoryName)
		{
			if (!string.IsNullOrEmpty(categoryName))
			{
				Category category = await _context.Categories
					.FirstOrDefaultAsync(c => c.Name == categoryName);

				if (category != null)
				{
					return true;
				}
			}

			return false;
		}

		#endregion
	}
}
