// -----------------------------------------------------------------------
// <copyright file="SmartPhoneRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{

	#region Usings

	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.Data.Entity;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using WebEngine.Core.Filters;

	#endregion

	/// <summary>
	/// <see cref="ProductRepository"/> class.
	/// </summary>
	public class ProductRepository : BaseRepository, IProductRepository
	{
		public ProductRepository(IServiceProvider services) : base(services)
		{
		}

		public async Task<bool> AddProductAsync(Product product)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateProductAsync(Product smartPhone)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteProductAsync(int productId)
		{
			throw new NotImplementedException();
		}

		public async Task<Product> GetProductAsync(int productId)
		{
			if (productId > DEFAULT_ID)
			{
				Product smartPhone = await _context.Products
					.Where(s => s.Id == productId)
					.Include(s => s.Company)
					.Include(s => s.ProductToProperty)
					.ThenInclude(sp => sp.Property)
					.ThenInclude(b => b.BaseProperty)
					.FirstOrDefaultAsync();

				return smartPhone;
			}

			return null;
		}

		public async Task<Product> GetProductAsync(string category, int productId)
		{
			Product product = await _context.Products
				.Where(s => s.Category.Name == category && s.Id == productId)
				.Include(s => s.Company)
				.Include(s => s.ProductToProperty)
				.ThenInclude(sp => sp.Property)
				.ThenInclude(b => b.BaseProperty)
				.FirstOrDefaultAsync();

			return product;
		}

		public async Task<Product> GetProductAsync(string category, string stringUrlName)
		{
			Product product = await _context.Products
				.Where(s => s.Category.Name == category && s.UrlName == stringUrlName)
				.Include(s => s.Company)
				.Include(s => s.ProductToProperty)
				.ThenInclude(sp => sp.Property)
				.ThenInclude(b => b.BaseProperty)
				.FirstOrDefaultAsync();

			return product;
		}

		public async Task<IList<Product>> GetProductsAsync(string category)
		{
			if (!string.IsNullOrEmpty(category))
			{
				IList<Product> products = await _context.Products
					.Where(p => p.Category.Name == category)
					.Select(p => new Product()
					{
						Category = p.Category,
						Id = p.Id,
						Name = p.Name,
						Company = p.Company,
						ProductToProperty = p.ProductToProperty
						.Where(pp => pp.Property.IsPreview == true)
						.Select(pp => new ProductToProperty()
						{
							Value = pp.Value,
							SizeValue = pp.SizeValue
						}).ToArray()
					}).ToArrayAsync();

				return products;
			}

			return null;
		}

		public async Task<IList<Product>> GetProductsAsync(ProductFilter filter)
		{
			if (filter != null)
			{
				//IEnumerable<Product> dbProducts = _context.Products.Where(p => p.Category.Name == filter.CategoryName)
				//	.Where(p => p.ProductToProperty.Any(e => e.ProductPropertyId == filter.Properties[0].PropertyId && e.Value == filter.Properties[0].Value))
				//	.Select(p => new Product()
				//	{
				//		Category = p.Category,
				//		Id = p.Id,
				//		Name = p.Name,
				//		Company = p.Company,
				//		ProductToProperty = p.ProductToProperty
				//		.Where(pp => pp.ProductProperty.IsPreview == true)
				//		.Select(pp => new ProductToProperty()
				//		{
				//			Value = pp.Value,
				//			SizeValue = pp.SizeValue
				//		}).ToArray()
				//	}).ToArray();


				IQueryable<Product> query = _context.Products.Where(p => p.Category.Name == filter.CategoryName);

				for (int i = 0; i < filter.Properties.Count; i++)
				{
					query = query.Where(p => p.ProductToProperty.Any(e => e.PropertyId == filter.Properties[i].PropertyId && e.Value == filter.Properties[i].Value));
				}

				try
				{
					//var x = query.Count();

					var result = query.Select(p => new Product()
					{
						Category = p.Category,
						Id = p.Id,
						Name = p.Name,
						Company = p.Company,
						ProductToProperty = p.ProductToProperty
						.Where(pp => pp.Property.IsPreview == true)
						.Select(pp => new ProductToProperty()
						{
							Value = pp.Value,
							SizeValue = pp.SizeValue
						}).ToArray()
					}).ToArray();
				}
				catch (Exception ex)
				{ }

				IList<Product> products = await _context.Products
				.Where(p => p.Category.Name == filter.CategoryName)
				.Select(p => new Product()
				{
					Category = p.Category,
					Id = p.Id,
					Name = p.Name,
					Company = p.Company,
					ProductToProperty = p.ProductToProperty
					.Where(pp => pp.Property.IsPreview == true)
					.Select(pp => new ProductToProperty()
					{
						Value = pp.Value,
						SizeValue = pp.SizeValue
					}).ToArray()
				}).ToArrayAsync();

				return products;
			}

			return null;
		}
	}
}
