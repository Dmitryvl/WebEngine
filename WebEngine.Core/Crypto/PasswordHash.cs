// -----------------------------------------------------------------------
// <copyright file="PasswordHash.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Crypto
{
	#region Usings

	using System.Security.Cryptography;
	using System.Text;

	#endregion

	/// <summary>
	/// <see cref="PasswordHash"/> class.
	/// </summary>
	public static class PasswordHash
	{
		/// <summary>
		/// Get SHA256 hash.
		/// </summary>
		/// <param name="value">Input string value.</param>
		/// <returns>Return hash.</returns>
		public static string GetSha256Hash(string value)
		{
			StringBuilder sb = new StringBuilder();

			using (SHA256 hash = SHA256.Create())
			{
				Encoding encoding = Encoding.UTF8;

				byte[] bytes = encoding.GetBytes(value);

				byte[] result = hash.ComputeHash(bytes);

				string pattern = "x2";

				int i = 0;

				while (i < result.Length)
				{
					sb.Append(result[i].ToString(pattern));

					i++;
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Get SHA256 hash.
		/// </summary>
		/// <param name="password">Password value.</param>
		/// <param name="passwordSalt">Password salt value.</param>
		/// <returns>Return hash.</returns>
		public static string GetSha256Hash(string password, string passwordSalt)
		{
			string value = string.Concat(password, passwordSalt);

			return GetSha256Hash(value);
		}
	}
}
