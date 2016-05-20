// -----------------------------------------------------------------------
// <copyright file="StoreRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;

	using WebEngine.Core.Config;
	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="StoreRepository"/> class.
	/// </summary>
	public class StoreRepository : BaseRepository, IStoreRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StoreRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public StoreRepository(IServiceProvider services, IOptions<AppConfig> config) : base(services, config)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add new store.
		/// </summary>
		/// <param name="store">New store.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> AddStore(Store store)
		{
			if (store != null && store.UserId > DEFAULT_ID)
			{
				Store dbStore = await _context.Stores
					.FirstOrDefaultAsync(s => s.Name == store.Name || s.UserId == store.UserId);

				if (dbStore == null)
				{
					store.IsActive = false;
					store.IsDeleted = false;
					store.CreationDate = DateTimeOffset.Now;

					_context.Stores.Add(store);

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Delete store.
		/// </summary>
		/// <param name="storeId">Store id.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> DeleteStore(int storeId)
		{
			if (storeId > DEFAULT_ID)
			{
				Store store = await _context.Stores
					.FirstOrDefaultAsync(s => s.Id == storeId);

				if (store != null)
				{
					store.IsActive = false;
					store.IsDeleted = true;

					_context.Entry(store).State = EntityState.Modified;

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get store by id.
		/// </summary>
		/// <param name="storeId">Store id.</param>
		/// <returns>Return store.</returns>
		public async Task<Store> GetStoreById(int storeId)
		{
			if (storeId > DEFAULT_ID)
			{
				Store store = await _context.Stores
					.AsNoTracking()
					.FirstOrDefaultAsync(s=>s.Id == storeId);

				return store;
			}

			return null;
		}

		/// <summary>
		/// Get store by name.
		/// </summary>
		/// <param name="storeName">Store name.</param>
		/// <returns>Return store.</returns>
		public async Task<Store> GetStoreByName(string storeName)
		{
			if (!string.IsNullOrEmpty(storeName))
			{
				Store store = await _context.Stores
					.AsNoTracking()
					.FirstOrDefaultAsync(s => s.Name == storeName);

				return store;
			}

			return null;
		}

		/// <summary>
		/// Update store.
		/// </summary>
		/// <param name="store">Changed store.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> UpdateStore(Store store)
		{
			if (store != null)
			{
				Store dbStore = await _context.Stores
					.FirstOrDefaultAsync(s => s.Id == store.Id);

				if (dbStore != null)
				{
					dbStore.Name = store.Name;

					_context.Entry(store).State = EntityState.Modified;

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}

		#endregion
	}
}
