// -----------------------------------------------------------------------
// <copyright file="ICategotyRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="ICategotyRepository"/> interface.
	/// </summary>
	public interface ICategotyRepository : IDisposable
	{
		/// <summary>
		/// Gets all categories.
		/// </summary>
		/// <returns>Return category collection.</returns>
		Task<IList<Category>> GetCategories();

		/// <summary>
		/// Checking category.
		/// </summary>
		/// <param name="categoryName">Category name.</param>
		/// <returns>Return result.</returns>
		Task<bool> IsExist(string categoryName);
	}
}
