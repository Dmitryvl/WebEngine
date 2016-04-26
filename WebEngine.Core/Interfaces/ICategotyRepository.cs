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
	/// TODO: Update summary.
	/// </summary>
	public interface ICategotyRepository : IDisposable
	{
		Task<IList<Category>> GetCategories();
	}
}
