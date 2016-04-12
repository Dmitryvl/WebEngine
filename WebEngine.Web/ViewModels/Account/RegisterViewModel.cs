// -----------------------------------------------------------------------
// <copyright file="RegisterViewModel.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web.ViewModels.Account
{
	#region Usings

	using System.ComponentModel.DataAnnotations;

	#endregion

	public class RegisterViewModel
	{
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
