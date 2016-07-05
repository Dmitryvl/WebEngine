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
						new Store() { Name = "Store", User = users[0], IsActive = true, IsDeleted = false, CreationDate = DateTimeOffset.Now }
					};

					Category[] categories = new Category[]
					{
						new Category() { Name = "smartphones", ViewName = "SmartPhones view name", IsActive = true },
						new Category() { Name = "tv", ViewName = "tv view name", IsActive = true },
						new Category() { Name = "pc", ViewName = "pc view name", IsActive = true }
					};

					BaseProperty[] productBaseProperty = new BaseProperty[]
					{
						new BaseProperty() { Name = "SmartPhoneBaseProperty", IsActive = true }
					};

					Property[] properties = new Property[]
					{
						new Property() { Name = "Screen size", IsActive = true, IsPreview = true, BaseProperty = productBaseProperty[0], DataType = dataTypes[1] },
						new Property() { Name = "Display resolution", IsActive = true, IsPreview = true, BaseProperty = productBaseProperty[0], DataType = dataTypes[1] },
						new Property() { Name = "Quantity of points of a matrix",IsPreview = true, IsActive = true, BaseProperty = productBaseProperty[0], DataType = dataTypes[1] },
						new Property() { Name = "Built-in camera", IsActive = true, IsPreview = false, BaseProperty = productBaseProperty[0], DataType = dataTypes[1] },
						new Property() { Name = "Technology of the screen", IsActive = true, IsPreview = true, BaseProperty = productBaseProperty[0], DataType = dataTypes[1] },
						new Property() { Name = "Amount of colors of the screen", IsActive = true, IsPreview = false, BaseProperty = productBaseProperty[0], DataType = dataTypes[1] }
					};

					Product[] products = new Product[]
					{
						new Product() { Category = categories[0], Name = "iPhone 5", UrlName = "iphone5", IsActive = true, ShortInfo = "4, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "iPhone 5s", UrlName = "iphone5s", IsActive = true, ShortInfo = "5, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "iPhone 6", UrlName = "iphone6", IsActive = true, ShortInfo = "5.1, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "iPhone 6s", UrlName = "iphone6s", IsActive = true, ShortInfo = "5.15, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "Galaxy s6", UrlName = "galaxys6", IsActive = true, ShortInfo = "5.7, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "Galaxy s7", UrlName = "galaxys7", IsActive = true, ShortInfo = "5.5, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "mx5", UrlName = "mx5", IsActive = true, ShortInfo = "5.1, 111x111, 12 Мg, IPS" },
						new Product() { Category = categories[0], Name = "z6", UrlName = "z6", IsActive = true, ShortInfo = "5.2, 111x111, 12 Мg, IPS" },
					};

					ProductToProperty[] ptp = new ProductToProperty[]
					{
						new ProductToProperty() { Product = products[0], Property = properties[0], Value = "4", SizeValue = "" },
						new ProductToProperty() { Product = products[0], Property = properties[1], Value = "111x111", SizeValue = "" },
						new ProductToProperty() { Product = products[0], Property = properties[2], Value = "12", SizeValue = "Mg" },
						new ProductToProperty() { Product = products[0], Property = properties[3], Value = "true", SizeValue = "Gb" },
						new ProductToProperty() { Product = products[0], Property = properties[4], Value = "IPS", SizeValue = "Gb" },
						new ProductToProperty() { Product = products[0], Property = properties[5], Value = "11", SizeValue = "m" }
					};

					ProductOffer[] productOffers = new ProductOffer[]
					{
						new ProductOffer() { Store = stores[0], Product = products[0], IsActive= true, Message = "message" }
					};

					ProductFilterItem[] productFilterItems = new ProductFilterItem[]
					{
						new ProductFilterItem() { Category = categories[0], Property = properties[0], FilterItemType = "DropDownList" },
						new ProductFilterItem() { Category = categories[0], Property = properties[1], FilterItemType = "Range" }
					};

					ProductFilterItemValue[] productFilterItemValues = new ProductFilterItemValue[]
					{
						new ProductFilterItemValue() { ProductFilterItem = productFilterItems[0], Value = "5.1" },
						new ProductFilterItemValue() { ProductFilterItem = productFilterItems[0], Value = "5.2" }
					};

					db.DataTypes.AddRange(dataTypes);
					db.Countries.AddRange(countries);
					db.Regions.AddRange(regions);
					db.Cities.AddRange(cities);
					db.Roles.AddRange(roles);
					db.Users.AddRange(users);
					db.Stores.AddRange(stores);
					db.Categories.AddRange(categories);
					db.BaseProperties.AddRange(productBaseProperty);
					db.Properties.AddRange(properties);
					db.Products.AddRange(products);
					db.ProductOffers.AddRange(productOffers);
					db.ProductToProperty.AddRange(ptp);
					db.ProductFilterItems.AddRange(productFilterItems);
					db.ProductFilterItemValue.AddRange(productFilterItemValues);

					await db.SaveChangesAsync();

					//for (int i = 0; i < 5; i++)
					//{
					//	Product[] p2 = new Product[900];

					//	for (int j = 0; j < 900; j++)
					//	{
					//		p2[j] = new Product() { CompanyId = 1, CategoryId = 1, Name = "test" + i, UrlName = "test", IsActive = true };
					//	}

					//	db.Products.AddRange(p2);

					//	await db.SaveChangesAsync();
					//}

					//for (int i = 0; i < 20; i++)
					//{
					//	Product[] p3 = new Product[900];

					//	for (int j = 0; j < 900; j++)
					//	{
					//		p3[j] = new Product() { CompanyId = 1, CategoryId = 2, Name = "test" + i, UrlName = "test", IsActive = true };
					//	}

					//	db.Products.AddRange(p3);

					//	await db.SaveChangesAsync();
					//}

					//for (int i = 1; i < 6; i++)
					//{
					//	ProductToProperty[] p3 = new ProductToProperty[990];

					//	for (int j = 0; j < 990; j++)
					//	{
					//		p3[j] = new ProductToProperty()
					//		{
					//			PropertyId = i,
					//			ProductId = j+4,
					//			Value = "11",
					//			SizeValue = ""
					//		};
					//	}

					//	db.ProductToProperty.AddRange(p3);

					//	await db.SaveChangesAsync();
					//}
				}
			}
		}
	}
}
