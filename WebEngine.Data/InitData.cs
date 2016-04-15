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

	using Microsoft.Extensions.DependencyInjection;

	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="InitData"/> class.
	/// </summary>
	public static class InitData
	{
		/// <summary>
		/// Initialize database.
		/// </summary>
		/// <param name="serviceProvider">Service provider.</param>
		/// <returns>Return result.</returns>
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
							Email = "admin@test.ru",
							IsActive = true,
							Password = "ada9c31f4cf484113b7fdddbae539ad9295415b92b8b7f95c05649f57c648596",
							PasswordSalt = "9297",
							Role = roles[0],
							IsDeleted = false,
							EmailKey = Guid.NewGuid(),
							RegisterDate = DateTimeOffset.Now
						},
						new User()
						{
							Name = "test",
							Email = "test@test.ru",
							IsActive = true,
							Password = "ada9c31f4cf484113b7fdddbae539ad9295415b92b8b7f95c05649f57c648596",
							PasswordSalt = "9297",
							Role = roles[1],
							IsDeleted = false,
							EmailKey = Guid.NewGuid(),
							RegisterDate = DateTimeOffset.Now
						},
					};

					Store[] stores = new Store[]
					{
						new Store() { Name = "Store1", IsActive = true, IsDeleted = false, CreationDate = DateTimeOffset.Now }
					};

					Company[] companies = new Company[]
					{
						new Company() { Name = "Company1", IsActive = true },
						new Company() { Name = "Company2", IsActive = true }
					};

					SmartPhone[] smartphones = new SmartPhone[]
					{
						new SmartPhone() { Company = companies[0], Name = "SM1", IsActive = true },
						new SmartPhone() { Company = companies[1], Name = "SM2", IsActive = true }
					};

					db.Roles.AddRange(roles);
					db.Users.AddRange(users);
					db.Stores.AddRange(stores);
					db.Companies.AddRange(companies);
					db.SmartPhones.AddRange(smartphones);
					

					await db.SaveChangesAsync();
				}
			}
		}
	}
}
