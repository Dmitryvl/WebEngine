// -----------------------------------------------------------------------
// <copyright file="InitData.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data
{

	#region Usings

	using System;
	using System.Threading.Tasks;
	using Core.Entities;
	using Microsoft.Extensions.DependencyInjection;
	#endregion

	/// <summary>
	/// <see cref="InitData"/> class.
	/// </summary>
	public static class InitData
	{
		public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
		{
			using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var db = serviceScope.ServiceProvider.GetService<WebEngineContext>();

				if (await db.Database.EnsureCreatedAsync())
				{
					Role[] roles = new Role[]
					{
						new Role() { Name = "admin", IsDeleted = false },
						new Role() { Name = "user", IsDeleted = false }
					};

					User[] users = new User[]
					{
						new User()
						{
							Name = "admin",
							Email ="admin@test.ru",
							IsActive = true,
							Password = "123",
							PasswordSalt = "",
							Role = roles[0],
							IsDeleted = false,
							EmailKey = Guid.NewGuid(),
							RegisterDate = DateTime.Now.Date
						},
						new User()
						{
							Name = "test",
							Email ="test@test.ru",
							IsActive = true,
							Password = "123",
							PasswordSalt = "",
							Role = roles[1],
							IsDeleted = false,
							EmailKey = Guid.NewGuid(),
							RegisterDate = DateTime.Now.Date
						},
					};

					db.Roles.AddRange(roles);
					db.Users.AddRange(users);

					await db.SaveChangesAsync();
				}
			}
		}
	}
}
