using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;

using Microsoft.AspNet.Mvc;

using Microsoft.Extensions.Logging;

using WebEngine.Web.ViewModels.Account;
using WebEngine.Data;

namespace WebEngine.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly ILogger _logger;
		WebEngineContext _context;

		public AccountController(
			ILoggerFactory loggerFactory,
			WebEngineContext context)
		{
			_logger = loggerFactory.CreateLogger<AccountController>();
			_context = context;
		}

		//
		// GET: /Account/Login
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			//if (ModelState.IsValid)
			//{

			//}

			var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
			if (user != null)
			{
				await Authenticate(model.Email); // аутентификация

				return RedirectToAction("Index", "Home");

			}
				

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		private async Task Authenticate(string userName)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// авторизаци пользователя
			await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
		}


		//
		// GET: /Account/Register
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{


			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			await HttpContext.Authentication.SignOutAsync("Cookies");
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}



		// GET: /Account/ConfirmEmail
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return View("Error");
			}



			return View();
		}



		#region Helpers

		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
		}

		#endregion
	}
}
