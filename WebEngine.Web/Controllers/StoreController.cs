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

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StoreController"/> class.
		/// </summary>
		/// <param name="storeRepository">Store repository.</param>
		public StoreController(IStoreRepository storeRepository)
		{
			_storeRepository = storeRepository;
		}

		#endregion

		#region Public methods

		public IActionResult Index()
		{
			return View();
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
