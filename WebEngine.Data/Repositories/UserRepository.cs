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
	using System;
	using System.Security.Cryptography;
	using Microsoft.AspNet.Cryptography.KeyDerivation;
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
						user.Password = Convert.ToBase64String(
							UserRepository.GetPasswordHash(user.Password, user.PasswordSalt));
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

		public async Task<User> GetUserByEmail(string userEmail)
		{
			if(!string.IsNullOrEmpty(userEmail))
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

		#endregion

		#region Private methods

		private string GetPasswordSalt()
		{
			Random rand = new Random();

			string salt = rand.Next(0, 9999).ToString();

			return salt;
		}

		private static byte[] GetPasswordHash(string password, string passwordSalt)
		{
			string p = password;//string.Concat(password, passwordSalt);
			int iterCount = 10000;
			int saltSize = 128 / 8;
			int numBytesRequested = 256 / 8;
			KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;

			RandomNumberGenerator rng = RandomNumberGenerator.Create();

			byte[] salt = new byte[saltSize];
			rng.GetBytes(salt);
			byte[] subkey = KeyDerivation.Pbkdf2(p, salt, prf, iterCount, numBytesRequested);

			var outputBytes = new byte[13 + salt.Length + subkey.Length];
			outputBytes[0] = 0x01; // format marker
			WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
			WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
			WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
			Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
			Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);

			rng.Dispose();

			return outputBytes;
		}

		private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
		{
			buffer[offset + 0] = (byte)(value >> 24);
			buffer[offset + 1] = (byte)(value >> 16);
			buffer[offset + 2] = (byte)(value >> 8);
			buffer[offset + 3] = (byte)(value >> 0);
		}

		private async Task<int> GetDefaultRoleId()
		{
			string defaultRoleName = "user";

			Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == defaultRoleName);

			if(role != null)
			{
				return role.Id;
			}

			return DEFAULT_ID;
		}

		#endregion
	}
}
