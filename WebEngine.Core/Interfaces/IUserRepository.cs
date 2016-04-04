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
		Task<bool> AddUser(User user);

		/// <summary>
		/// Update user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		Task<bool> Update(User user);

		/// <summary>
		/// Delete user
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		Task<bool> Delete(int userId);

		/// <summary>
		/// Get user by identifier.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		Task<User> GetUserById(int userId);
	}
}
