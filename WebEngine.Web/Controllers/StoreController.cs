// -----------------------------------------------------------------------
// <copyright file="StoreController.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.Controllers
{
	#region Usings

	using System.Threading.Tasks;

	using Microsoft.AspNet.Authorization;
	using Microsoft.AspNet.Mvc;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Web.ViewModels.Store;

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

		[HttpGet, Route("[controller]{storeId:int}")]
		public async Task<IActionResult> GetStore(int storeId)
		{
			if (storeId > 0)
			{
				Store store = await _storeRepository.GetStoreById(storeId);

				if (store != null)
				{
					StoreView model = new StoreView();

					model.StoreId = store.Id;
					model.StoreName = store.Name;
					model.CreationDate = store.CreationDate;

					return View("Store", model);
				}
			}

			return View("Error");
		}

		[HttpGet, Route("[controller]/{storeName}")]
		public async Task<IActionResult> GetStore(string storeName)
		{
			if (!string.IsNullOrEmpty(storeName))
			{
				Store store = await _storeRepository.GetStoreByName(storeName);

				if (store != null)
				{
					StoreView model = new StoreView();

					model.StoreId = store.Id;
					model.StoreName = store.Name;
					model.CreationDate = store.CreationDate;

					return View("Store", model);
				}
			}

			return View("Error");
		}

		[HttpGet, Authorize, Route("[controller]/CreateStore")]
		public IActionResult CreateStore()
		{
			return View();
		}

		[HttpPost, Authorize, ValidateAntiForgeryToken, Route("[controller]/CreateStore")]
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
				_userRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		#endregion
	}
}
