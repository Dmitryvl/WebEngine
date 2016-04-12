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

	public class LoginViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		//[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}
