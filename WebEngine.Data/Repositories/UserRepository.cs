﻿// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.Data.Entity;

	using WebEngine.Core.Crypto;
	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="UserRepository"/> class.
	/// </summary>
	public class UserRepository : BaseRepository, IUserRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository" /> class.
		/// </summary>
		/// <param name="services">Service provider.</param>
		public UserRepository(IServiceProvider services) : base(services)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add new user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> AddUser(User user)
		{
			if (user != null)
			{
				User dbUser = await _context.Users
					.FirstOrDefaultAsync(u => u.Name == user.Name || u.Email == user.Email);

				if (dbUser == null)
				{
					int defaultRoleId = await GetDefaultRoleId();

					if (defaultRoleId > DEFAULT_ID)
					{
						user.PasswordSalt = GetPasswordSalt();
						user.Password = PasswordHash.GetSha256Hash(user.Password, user.PasswordSalt);
						user.RegisterDate = DateTime.Now;
						user.IsActive = false;
						user.IsDeleted = false;
						user.EmailKey = Guid.NewGuid();
						user.RoleId = defaultRoleId;

						_context.Users.Add(user);

						await _context.SaveChangesAsync();

						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Delete user
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> DeleteUser(int userId)
		{
			if (userId > DEFAULT_ID)
			{
				User user = await _context.Users
					.FirstOrDefaultAsync(u => u.Id == userId);

				if (user != null)
				{
					user.IsDeleted = true;

					_context.Entry(user).State = EntityState.Modified;

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get user by email.
		/// </summary>
		/// <param name="userEmail">User email.</param>
		/// <returns>Return user.</returns>
		public async Task<User> GetUserByEmail(string userEmail)
		{
			if (!string.IsNullOrEmpty(userEmail))
			{
				return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
			}

			return null;
		}

		/// <summary>
		/// Get user by identifier.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		public async Task<User> GetUserById(int userId)
		{
			if (userId > DEFAULT_ID)
			{
				User user = await _context.Users
					.FirstOrDefaultAsync(u => u.Id == userId);

				return user;
			}

			return null;
		}

		/// <summary>
		/// Get user by name.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <returns>Return user.</returns>
		public async Task<User> GetUserByName(string userName)
		{
			if (!string.IsNullOrEmpty(userName))
			{
				return await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);
			}

			return null;
		}

		/// <summary>
		/// Update user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> UpdateUser(User user)
		{
			if (user != null)
			{
				User dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

				if (dbUser != null)
				{
					dbUser.Name = user.Name;

					_context.Entry(dbUser).State = EntityState.Modified;

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get valid user if exist.
		/// </summary>
		/// <param name="userEmail">User email</param>
		/// <param name="password">Password value.</param>
		/// <returns>Return user.</returns>
		public async Task<User> GetValidUser(string userEmail, string password)
		{
			if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(password))
			{
				User user = await _context.Users
					.Where(u => u.Email == userEmail && u.IsActive == true && u.IsDeleted == false)
					.Select(u => new User()
					{
						Id = u.Id,
						Name = u.Name,
						Email = u.Email,
						Password = u.Password,
						PasswordSalt = u.PasswordSalt,
						Role = u.Role
					}).FirstOrDefaultAsync();

				if (user != null && user.Password == PasswordHash.GetSha256Hash(password, user.PasswordSalt))
				{
					return user;
				}
			}

			return null;
		}

		/// <summary>
		/// User activation by email key.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="emailKey">Email key.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> UserActivation(int userId, Guid emailKey)
		{
			if (userId > DEFAULT_ID && emailKey != Guid.Empty)
			{
				User user = await _context.Users
					.FirstOrDefaultAsync(u => u.Id == userId && u.EmailKey == emailKey);

				if (user != null)
				{
					user.IsActive = true;
					user.EmailKey = null;

					_context.Entry(user).State = EntityState.Modified;

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}


		#endregion

		#region Private methods

		/// <summary>
		/// Get new password salt.
		/// </summary>
		/// <returns>Return password salt.</returns>
		private string GetPasswordSalt()
		{
			Random rand = new Random();

			string salt = rand.Next(0, 9999).ToString();

			return salt;
		}

		/// <summary>
		/// Get default role id.
		/// </summary>
		/// <returns>Return role id.</returns>
		private async Task<int> GetDefaultRoleId()
		{
			const string defaultRoleName = "user";

			Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == defaultRoleName);

			if (role != null)
			{
				return role.Id;
			}

			return DEFAULT_ID;
		}

		#endregion
	}
}
