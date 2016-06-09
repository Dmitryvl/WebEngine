// -----------------------------------------------------------------------
// <copyright file="IProductFilterRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Entities;

	#endregion

	/// <summary>
	/// <see cref="IProductFilterRepository"/> interface.
	/// </summary>
	public interface IProductFilterRepository : IDisposable
	{
		Task<IList<ProductFilterItem>> GetProductFilterItems(int categoryId);
	}
}
