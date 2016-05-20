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
		/// <summary>
		/// Add new store.
		/// </summary>
		/// <param name="store">New store.</param>
		/// <returns>Return result.</returns>
		Task<bool> AddStore(Store store);

		/// <summary>
		/// Delete store.
		/// </summary>
		/// <param name="storeId">Store id.</param>
		/// <returns>Return result.</returns>
		Task<bool> DeleteStore(int storeId);

		/// <summary>
		/// Update store.
		/// </summary>
		/// <param name="store">Changed store.</param>
		/// <returns>Return result.</returns>
		Task<bool> UpdateStore(Store store);

		/// <summary>
		/// Get store by id.
		/// </summary>
		/// <param name="storeId">Store id.</param>
		/// <returns>Return store.</returns>
		Task<Store> GetStoreById(int storeId);

		/// <summary>
		/// Get store by name.
		/// </summary>
		/// <param name="storeName">Store name.</param>
		/// <returns>Return store.</returns>
		Task<Store> GetStoreByName(string storeName);
	}
}
