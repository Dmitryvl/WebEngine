// -----------------------------------------------------------------------
// <copyright file="StoreRepository.cs" author="Dzmitry Prakapenka">
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
	using WebEngine.Core.Interfaces;
	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="StoreRepository"/> class.
	/// </summary>
	public class StoreRepository : BaseRepository, IStoreRepository
	{
		#region Constructors

		public StoreRepository(IServiceProvider services) : base(services)
		{
		}

		#endregion

		#region Public methods

		public async Task<bool> AddStore(Store store)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteStore(int storeId)
		{
			throw new NotImplementedException();
		}

		public async Task<Store> GetStoreById(int storeId)
		{
			throw new NotImplementedException();
		}

		public async Task<Store> GetStoreByName(string storeName)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateStore(Store store)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
