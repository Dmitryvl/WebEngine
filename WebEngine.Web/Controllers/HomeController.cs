// -----------------------------------------------------------------------
// <copyright file="HomeController.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.Controllers
{
	#region Usings

	using Microsoft.AspNet.Mvc;
	using Microsoft.AspNet.Authorization;

	#endregion

	/// <summary>
	/// <see cref="HomeController"/> class.
	/// </summary>
	public class HomeController : Controller
	{
		#region Private fields

		#endregion

		#region Constructors

		#endregion

		#region Public methods

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}

		[Authorize(Roles = "admin")]
		public string Test()
		{
			return "ok";
		}

		#endregion

		#region Override methods

		/// <summary>
		/// Override <see cref="Dispose"/> method.
		/// </summary>
		/// <param name="disposing">is disposing.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion
	}
}
