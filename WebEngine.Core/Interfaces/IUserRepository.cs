﻿// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="IUserRepository"/> interface.
	/// </summary>
	public interface IUserRepository : IDisposable
	{
		/// <summary>
		/// Add new user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		Task<bool> AddUserAsync(User user);

		/// <summary>
		/// Update user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		Task<bool> UpdateUserAsync(User user);

		/// <summary>
		/// Delete user
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		Task<bool> DeleteUserAsync(int userId);

		/// <summary>
		/// Get user by identifier.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		Task<User> GetUserByIdAsync(int userId);

		/// <summary>
		/// Get user by name.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <returns>Return user.</returns>
		Task<User> GetUserByNameAsync(string userName);

		/// <summary>
		/// Get user by email.
		/// </summary>
		/// <param name="userEmail">User email.</param>
		/// <returns>Return user.</returns>
		Task<User> GetUserByEmailAsync(string userEmail);

		/// <summary>
		/// Get valid user if exist.
		/// </summary>
		/// <param name="userEmail">User email</param>
		/// <param name="password">Password value.</param>
		/// <returns>Return user.</returns>
		Task<User> GetValidUserAsync(string userEmail, string password);

		/// <summary>
		/// User activation.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="emailKey">Email key.</param>
		/// <returns>Return result.</returns>
		Task<bool> UserActivationAsync(int userId, Guid emailKey);

		/// <summary>
		/// Get user id by user name.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <returns>Return user id.</returns>
		Task<int> GetUserIdByUserNameAsync(string userName);
	}
}
