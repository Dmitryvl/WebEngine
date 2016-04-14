// -----------------------------------------------------------------------
// <copyright file="StoreController.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Mvc;
	using WebEngine.Core.Interfaces;
	using Microsoft.AspNet.Authorization;
	using ViewModels.Store;
	using Core.Entities;
	#endregion

	/// <summary>
	/// <see cref="StoreController"/> class.
	/// </summary>
	public class StoreController : Controller
	{
		#region Private fields

		/// <summary>
		/// Store repository.
		/// </summary>
		private readonly IStoreRepository _storeRepository;

		/// <summary>
		/// User repository.
		/// </summary>
		private readonly IUserRepository _userRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StoreController"/> class.
		/// </summary>
		/// <param name="storeRepository">Store repository.</param>
		public StoreController(
			IStoreRepository storeRepository,
			IUserRepository userRepository)
		{
			_storeRepository = storeRepository;
			_userRepository = userRepository;
		}

		#endregion

		#region Public methods

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet, Authorize]
		public IActionResult CreateStore()
		{
			return View();
		}

		[HttpPost, Authorize, ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStore(CreateStore newStore)
		{
			if (newStore != null)
			{
				int userId = await _userRepository.GetUserIdByUserName(User.Identity.Name);

				Store store = new Store();

				store.Name = newStore.StoreName;
				store.UserId = userId;

				bool isSuccess = await _storeRepository.AddStore(store);

				if (isSuccess)
				{
					return RedirectToAction("Profile", "Account");
				}
			}

			return View("Error");
		}

		#endregion

		#region Override methods

		/// <summary>
		/// Override <see cref="Dispose"/> method.
		/// </summary>
		/// <param name="disposing">is disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_storeRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		#endregion
	}
}
