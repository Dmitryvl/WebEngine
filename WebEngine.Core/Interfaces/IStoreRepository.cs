// -----------------------------------------------------------------------
// <copyright file="IStoreRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	
	#region Usings

	using System;
	using System.Threading.Tasks;

	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="IStoreRepository"/> interface.
	/// </summary>
	public interface IStoreRepository : IDisposable
	{
		Task<bool> AddStore(Store store);

		Task<bool> DeleteStore(int storeId);

		Task<bool> UpdateStore(Store store);

		Task<Store> GetStoreById(int storeId);

		Task<Store> GetStoreByName(string storeName);
	}
}
