// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System.Threading.Tasks;

	using Microsoft.Data.Entity;
	using Microsoft.Extensions.Logging;

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
		/// <param name="context">Database context.</param>
		/// <param name="loggerFactory">Logger factory</param>
		public UserRepository(WebEngineContext context, ILoggerFactory loggerFactory) : base(context, loggerFactory)
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
					.FirstOrDefaultAsync();

				if (dbUser == null)
				{
					_context.Users.Add(user);

					await _context.SaveChangesAsync();

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Delete user
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> Delete(int userId)
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
		/// Update user.
		/// </summary>
		/// <param name="user">User entity.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> Update(User user)
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

		#endregion
	}
}
