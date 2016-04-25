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
					Country[] countries = new Country[]
					{
						new Country() { Name = "Country", IsActive = true }
					};

					Region[] regions = new Region[]
					{
						new Region() { Name = "Region", Country = countries[0], IsActive = true }
					};

					City[] cities = new City[]
					{
						new City() { Name = "City", Region = regions[0], IsActive = true }
					};

					DataType[] dataTypes = new DataType[]
					{
						new DataType() { Name = "Int" },
						new DataType() { Name = "Float" },
						new DataType() { Name = "DateTimeOffset" },
						new DataType() { Name = "String" }
					};

					Role[] roles = new Role[]
					{
						new Role() { Name = "admin", IsActive = false },
						new Role() { Name = "user", IsActive = false }
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
						new Store() { Name = "Store1", User = users[0], IsActive = true, IsDeleted = false, CreationDate = DateTimeOffset.Now }
					};

					Company[] companies = new Company[]
					{
						new Company() { Name = "Company1", IsActive = true },
						new Company() { Name = "Company2", IsActive = true }
					};

					Category[] categories = new Category[]
					{
						new Category() { Name = "SmartPhone", IsActive = true },
						new Category() { Name = "TV", IsActive = true },
						new Category() { Name = "PC", IsActive = true }
					};

					ProductBaseProperty[] productBaseProperty = new ProductBaseProperty[]
					{
						new ProductBaseProperty() { Name = "SmartPhoneBaseProperty", IsActive = true }
					};

					ProductProperty[] pp = new ProductProperty[]
					{
						new ProductProperty() { Name = "ProductProperty", IsActive = true, ProductBaseProperty = productBaseProperty[0], DataType = dataTypes[1] }
					};

					Product[] products = new Product[]
					{
						new Product() { CompanyId = 1, Category = categories[0], Name = "SM", IsActive = true }
					};

					ProductToProperty[] ptp = new ProductToProperty[]
					{
						new ProductToProperty() { Product = products[0], ProductProperty = pp[0], Value = "111", SizeValue = "Gb" }
					};

					ProductOffer[] productOffers = new ProductOffer[]
					{
						new ProductOffer() { Store = stores[0], Product = products[0], IsActive= true, Message = "message" }
					};

					db.DataTypes.AddRange(dataTypes);
					db.Countries.AddRange(countries);
					db.Regions.AddRange(regions);
					db.Cities.AddRange(cities);
					db.Roles.AddRange(roles);
					db.Users.AddRange(users);
					db.Stores.AddRange(stores);
					db.Companies.AddRange(companies);
					db.Categories.AddRange(categories);
					db.ProductBaseProperties.AddRange(productBaseProperty);
					db.ProductProperties.AddRange(pp);
					db.Products.AddRange(products);
					db.ProductOffers.AddRange(productOffers);
					db.ProductToProperty.AddRange(ptp);

					await db.SaveChangesAsync();

				}
			}
		}
	}
}
