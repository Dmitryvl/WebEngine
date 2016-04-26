// <copyright file="CategoryRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{

	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Core.Entities;
	using WebEngine.Core.Interfaces;
	using Microsoft.Data.Entity;
	#endregion

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class CategoryRepository : BaseRepository, ICategotyRepository
	{
		// Empty.
		public CategoryRepository(IServiceProvider services) : base(services)
		{
		}

		public async Task<IList<Category>> GetCategories()
		{
			return await _context.Categories.ToArrayAsync();
		}
	}
}
