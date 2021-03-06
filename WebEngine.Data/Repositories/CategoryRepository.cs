﻿// <copyright file="CategoryRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using Microsoft.Extensions.Logging;

	#endregion

	/// <summary>
	/// <see cref="CategoryRepository"/> class.
	/// </summary>
	public class CategoryRepository : BaseRepository<CategoryRepository>, ICategotyRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public CategoryRepository(IServiceProvider services) : base(services)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get categories.
		/// </summary>
		/// <returns>Return category collection.</returns>
		public async Task<IList<Category>> GetCategoriesAsync()
		{
			try
			{
				Category[] categories = await _context.Categories
					.ToArrayAsync()
					.ConfigureAwait(false);

				return categories;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return null;
			}
		}

		/// <summary>
		/// Get category by id.
		/// </summary>
		/// <param name="categoryId">Category id.</param>
		/// <returns>Return category.</returns>
		public async Task<Category> GetCategoryAsync(int categoryId)
		{
			if (categoryId > DEFAULT_ID)
			{
				try
				{
					Category category = await _context.Categories
						.FirstOrDefaultAsync(c => c.Id == categoryId)
						.ConfigureAwait(false);

					return category;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return null;
				}
			}

			_logger.LogWarning("GetCategoryAsync: categoryId <= 0");

			return null;
		}

		/// <summary>
		/// Get category by name.
		/// </summary>
		/// <param name="categoryName">Category name.</param>
		/// <returns>Return category.</returns>
		public async Task<Category> GetCategoryAsync(string categoryName)
		{
			if (!string.IsNullOrEmpty(categoryName))
			{
				try
				{
					Category category = await _context.Categories
						.FirstOrDefaultAsync(c => c.Name == categoryName)
						.ConfigureAwait(false);

					return category;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return null;
				}
			}

			_logger.LogWarning("GetCategoryAsync: categoryName is null or empty!");

			return null;
		}

		/// <summary>
		/// Checking category by name.
		/// </summary>
		/// <param name="categoryName">Category name.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> IsExistAsync(string categoryName)
		{
			if (!string.IsNullOrEmpty(categoryName))
			{
				try
				{
					Category category = await _context.Categories
						.FirstOrDefaultAsync(c => c.Name == categoryName)
						.ConfigureAwait(false);

					if (category != null)
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return false;
				}
			}

			_logger.LogWarning("IsExistAsync: categoryName is null or empty!");

			return false;
		}

		#endregion
	}
}
