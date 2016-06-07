// -----------------------------------------------------------------------
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

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;

	using WebEngine.Core.Config;
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
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public UserRepository(IServiceProvider services, IOptions<AppConfig> config) : base(services, config)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add new user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> AddUserAsync(User user)
		{
			if (user != null)
			{
				User dbUser = await _context.Users
					.FirstOrDefaultAsync(u => u.Name == user.Name || u.Email == user.Email)
					.ConfigureAwait(false);

				if (dbUser == null)
				{
					int defaultRoleId = await GetDefaultRoleIdAsync().ConfigureAwait(false);

					if (defaultRoleId > DEFAULT_ID)
					{
						user.PasswordSalt = GetPasswordSalt();
						user.Password = PasswordHash.GetSha256Hash(user.Password, user.PasswordSalt);
						user.RegisterDate = DateTimeOffset.Now;
						user.IsActive = false;
						user.IsDeleted = false;
						user.EmailKey = Guid.NewGuid();
						user.RoleId = defaultRoleId;

						_context.Users.Add(user);

						await _context.SaveChangesAsync().ConfigureAwait(false);

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
		public async Task<bool> DeleteUserAsync(int userId)
		{
			if (userId > DEFAULT_ID)
			{
				User user = await _context.Users
					.FirstOrDefaultAsync(u => u.Id == userId)
					.ConfigureAwait(false);

				if (user != null)
				{
					user.IsDeleted = true;

					_context.Entry(user).State = EntityState.Modified;

					await _context.SaveChangesAsync().ConfigureAwait(false);

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
		public async Task<User> GetUserByEmailAsync(string userEmail)
		{
			if (!string.IsNullOrEmpty(userEmail))
			{
				return await _context.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Email == userEmail)
					.ConfigureAwait(false);
			}

			return null;
		}

		/// <summary>
		/// Get user by identifier.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		public async Task<User> GetUserByIdAsync(int userId)
		{
			if (userId > DEFAULT_ID)
			{
				User user = await _context.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Id == userId)
					.ConfigureAwait(false);

				return user;
			}

			return null;
		}

		/// <summary>
		/// Get user by name.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <returns>Return user.</returns>
		public async Task<User> GetUserByNameAsync(string userName)
		{
			if (!string.IsNullOrEmpty(userName))
			{
				User user = await _context.Users
					.AsNoTracking()
					.Where(u => u.Name == userName && u.IsActive == true && u.IsDeleted == false)
					.Select(u => new User()
					{
						Id = u.Id,
						Name = u.Name,
						Email = u.Email,
						Password = u.Password,
						PasswordSalt = u.PasswordSalt,
						RegisterDate = u.RegisterDate,
						Role = u.Role,
					})
					.FirstOrDefaultAsync()
					.ConfigureAwait(false);

				if (user != null)
				{
					user.Stores = await _context.Stores
						.Where(s => s.UserId == user.Id)
						.Select(s => new Store()
						{
							Id = s.Id,
							Name = s.Name,
							CreationDate = s.CreationDate
						})
						.ToArrayAsync()
						.ConfigureAwait(false);
				}

				return user;
			}

			return null;
		}

		/// <summary>
		/// Update user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> UpdateUserAsync(User user)
		{
			if (user != null)
			{
				User dbUser = await _context.Users
					.FirstOrDefaultAsync(u => u.Id == user.Id)
					.ConfigureAwait(false);

				if (dbUser != null)
				{
					dbUser.Name = user.Name;

					_context.Entry(dbUser).State = EntityState.Modified;

					await _context.SaveChangesAsync().ConfigureAwait(false);

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
		public async Task<User> GetValidUserAsync(string userEmail, string password)
		{
			if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(password))
			{
				User user = await _context.Users
					.AsNoTracking()
					.Where(u => u.Email == userEmail && u.IsActive == true && u.IsDeleted == false)
					.Select(u => new User()
					{
						Id = u.Id,
						Name = u.Name,
						Email = u.Email,
						Password = u.Password,
						PasswordSalt = u.PasswordSalt,
						Role = u.Role
					})
					.FirstOrDefaultAsync()
					.ConfigureAwait(false);

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
		public async Task<bool> UserActivationAsync(int userId, Guid emailKey)
		{
			if (userId > DEFAULT_ID && emailKey != Guid.Empty)
			{
				User user = await _context.Users
					.FirstOrDefaultAsync(u => u.Id == userId && u.EmailKey == emailKey)
					.ConfigureAwait(false);

				if (user != null)
				{
					user.IsActive = true;
					user.EmailKey = null;

					_context.Entry(user).State = EntityState.Modified;

					await _context.SaveChangesAsync().ConfigureAwait(false);

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get user id by name.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <returns>Return user id.</returns>
		public async Task<int> GetUserIdByUserNameAsync(string userName)
		{
			if (!string.IsNullOrEmpty(userName))
			{
				User user = await _context.Users
					.AsNoTracking()
					.FirstOrDefaultAsync(u => u.Name == userName)
					.ConfigureAwait(false);

				if (user != null)
				{
					return user.Id;
				}
			}

			return DEFAULT_ID;
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
		private async Task<int> GetDefaultRoleIdAsync()
		{
			const string defaultRoleName = "user";

			Role role = await _context.Roles
				.AsNoTracking()
				.FirstOrDefaultAsync(r => r.Name == defaultRoleName)
				.ConfigureAwait(false);

			if (role != null)
			{
				return role.Id;
			}

			return DEFAULT_ID;
		}

		#endregion
	}
}
