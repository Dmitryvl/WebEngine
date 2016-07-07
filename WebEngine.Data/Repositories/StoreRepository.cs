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

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using Microsoft.Extensions.Logging;

	#endregion

	/// <summary>
	/// <see cref="StoreRepository"/> class.
	/// </summary>
	public class StoreRepository : BaseRepository<StoreRepository>, IStoreRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StoreRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public StoreRepository(IServiceProvider services) : base(services)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add new store.
		/// </summary>
		/// <param name="store">New store.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> AddStoreAsync(Store store)
		{
			if (store != null && store.UserId > DEFAULT_ID)
			{
				try
				{
					Store dbStore = await _context.Stores
						.FirstOrDefaultAsync(s => s.Name == store.Name || s.UserId == store.UserId)
						.ConfigureAwait(false);

					if (dbStore == null)
					{
						store.IsActive = false;
						store.IsDeleted = false;
						store.CreationDate = DateTimeOffset.Now;

						_context.Stores.Add(store);

						await _context.SaveChangesAsync().ConfigureAwait(false);

						return true;
					}

					_logger.LogWarning("AddStoreAsync: store is exist!");

					return false;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return false;
				}
			}

			_logger.LogWarning("AddStoreAsync: store is null OR userId <= 0");

			return false;
		}

		/// <summary>
		/// Delete store.
		/// </summary>
		/// <param name="storeId">Store id.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> DeleteStoreAsync(int storeId)
		{
			if (storeId > DEFAULT_ID)
			{
				try
				{
					Store store = await _context.Stores
						.FirstOrDefaultAsync(s => s.Id == storeId)
						.ConfigureAwait(false);

					if (store != null)
					{
						store.IsActive = false;
						store.IsDeleted = true;

						_context.Entry(store).State = EntityState.Modified;

						await _context.SaveChangesAsync().ConfigureAwait(false);

						return true;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return false;
				}
			}

			_logger.LogWarning("DeleteStoreAsync: storeId <= 0");

			return false;
		}

		/// <summary>
		/// Get store by id.
		/// </summary>
		/// <param name="storeId">Store id.</param>
		/// <returns>Return store.</returns>
		public async Task<Store> GetStoreByIdAsync(int storeId)
		{
			if (storeId > DEFAULT_ID)
			{
				try
				{
					Store store = await _context.Stores
						.FirstOrDefaultAsync(s => s.Id == storeId)
						.ConfigureAwait(false);

					return store;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return null;
				}
			}

			_logger.LogWarning("GetStoreByIdAsync: storeId <= 0");

			return null;
		}

		/// <summary>
		/// Get store by name.
		/// </summary>
		/// <param name="storeName">Store name.</param>
		/// <returns>Return store.</returns>
		public async Task<Store> GetStoreByNameAsync(string storeName)
		{
			if (!string.IsNullOrEmpty(storeName))
			{
				try
				{
					Store store = await _context.Stores
						.FirstOrDefaultAsync(s => s.Name == storeName)
						.ConfigureAwait(false);

					return store;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return null;
				}
			}

			_logger.LogWarning("GetStoreByNameAsync: storeName is null or empty!");

			return null;
		}

		/// <summary>
		/// Update store.
		/// </summary>
		/// <param name="store">Changed store.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> UpdateStoreAsync(Store store)
		{
			if (store != null)
			{
				try
				{
					Store dbStore = await _context.Stores
						.FirstOrDefaultAsync(s => s.Id == store.Id)
						.ConfigureAwait(false);

					if (dbStore != null)
					{
						dbStore.Name = store.Name;

						_context.Entry(store).State = EntityState.Modified;

						await _context.SaveChangesAsync().ConfigureAwait(false);

						return true;
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return false;
				}
			}

			_logger.LogWarning("UpdateStoreAsync: store is null!");

			return false;
		}

		#endregion
	}
}
