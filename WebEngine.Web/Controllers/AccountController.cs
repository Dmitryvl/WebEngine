﻿// -----------------------------------------------------------------------
// <copyright file="AccountController.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.Controllers
{
	#region Usings

	using System;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Web.ViewModels.Account;

	#endregion

	/// <summary>
	/// <see cref="AccountController"/> class.
	/// </summary>
	[Authorize]
	public class AccountController : Controller
	{
		#region Private fields

		/// <summary>
		/// User repository.
		/// </summary>
		private readonly IUserRepository _userRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <param name="userRepository">User repository.</param>
		public AccountController(
			IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		#endregion

		#region Public methods

		public async Task<IActionResult> Profile()
		{
			User user = await _userRepository.GetUserByNameAsync(HttpContext.User.Identity.Name);

			if (user != null)
			{
				ProfileView profile = new ProfileView();

				profile.UserId = user.Id;
				profile.UserName = user.Name;
				profile.RoleId = user.RoleId;
				profile.RoleName = user.Role.Name;
				profile.RegisterDate = user.RegisterDate;
				profile.Stores = user.Stores.Select(s => new UserStoreView()
				{
					StoreId = s.Id,
					StoreName = s.Name,
					CreationDate = s.CreationDate
				});

				return View(profile);
			}

			return View("Error");
		}

		[HttpGet, AllowAnonymous, Route("about/{userName}")]
		public async Task<IActionResult> GetUserProfile(string userName)
		{
			if (!string.IsNullOrEmpty(userName))
			{
				User user = await _userRepository.GetUserByNameAsync(userName);

				if (user != null)
				{
					ProfileView profile = new ProfileView();

					profile.UserId = user.Id;
					profile.UserName = user.Name;
					profile.RoleId = user.RoleId;
					profile.RoleName = user.Role.Name;
					profile.RegisterDate = user.RegisterDate;
					profile.Stores = user.Stores.Select(s => new UserStoreView()
					{
						StoreId = s.Id,
						StoreName = s.Name,
						CreationDate = s.CreationDate
					});

					return View("Profile", profile);
				}
			}

			return View("Error");
		}

		/// <summary>
		/// User login.
		/// </summary>
		/// <param name="returnUrl">Return url.</param>
		/// <returns>Return action result.</returns>
		[HttpGet, AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;

			return View();
		}

		/// <summary>
		/// User login.
		/// </summary>
		/// <param name="model">Login view model</param>
		/// <param name="returnUrl">Return url.</param>
		/// <returns>Return action result.</returns>
		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;

			if (ModelState.IsValid)
			{
				User user = await _userRepository.GetValidUserAsync(model.Email, model.Password);

				if (user != null)
				{
					await Authenticate(user.Name, user.Role.Name);

					return Redirect(returnUrl);
				}
			}

			return View(model);
		}

		/// <summary>
		/// User registration.
		/// </summary>
		/// <returns>Return action result.</returns>
		[HttpGet, AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		/// <summary>
		/// User registration.
		/// </summary>
		/// <param name="model">Register view model.</param>
		/// <returns>Return action result.</returns>
		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = new User()
				{
					Name = model.Name,
					Email = model.Email,
					Password = model.Password
				};

				bool isSuccess = await _userRepository.AddUserAsync(user);

				if (isSuccess)
				{
					return RedirectToAction("Index", "Home");
				}
			}

			return View(model);
		}

		/// <summary>
		/// Log off.
		/// </summary>
		/// <returns>Return action result.</returns>
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			await HttpContext.Authentication.SignOutAsync("Cookies");

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		/// <summary>
		/// Confirm email and user activation.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="emailKey">Email key.</param>
		/// <returns>Return action result.</returns>
		[HttpGet, AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(int userId, Guid emailKey)
		{
			if (userId > 0 || emailKey != Guid.Empty)
			{
				bool isActivated = await _userRepository.UserActivationAsync(userId, emailKey);

				if (isActivated)
				{
					return View();
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
				_userRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Set cookies.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <param name="roleName">User role.</param>
		/// <returns>Return result.</returns>
		private async Task Authenticate(string userName, string roleName)
		{
			Claim[] claims = new Claim[]
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)
			};

			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

			await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
		}

		/// <summary>
		/// Redirect to local.
		/// </summary>
		/// <param name="returnUrl">Return url.</param>
		/// <returns>Return action result.</returns>
		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}

			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		#endregion
	}
}
