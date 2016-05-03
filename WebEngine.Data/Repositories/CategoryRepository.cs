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

	using Microsoft.Data.Entity;
	using Microsoft.Extensions.OptionsModel;

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
			return await _context.Categories.ToArrayAsync();
		}

		#endregion
	}
}
