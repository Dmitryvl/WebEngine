// -----------------------------------------------------------------------
// <copyright file="SmartPhoneRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{

	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Microsoft.Data.Entity;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;
	using System.Linq;
	#endregion

	/// <summary>
	/// <see cref="ProductRepository"/> class.
	/// </summary>
	public class ProductRepository : BaseRepository, IProductRepository
	{
		public ProductRepository(IServiceProvider services) : base(services)
		{
		}

		public async Task<bool> AddProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteProduct(int productId)
		{
			throw new NotImplementedException();
		}

		public async Task<Product> GetProduct(int productId)
		{
			if (productId > DEFAULT_ID)
			{
				Product smartPhone = await _context.Products
					.Where(s => s.Id == productId)
					.Include(s => s.ProductToProperty)
					.ThenInclude(sp => sp.ProductProperty)
					.ThenInclude(b => b.ProductBaseProperty)
					.FirstOrDefaultAsync();

				return smartPhone;
			}

			return null;
		}

		public async Task<Product> GetProduct(string category, int productId)
		{
			Product product = await _context.Products
				.Where(s => s.Category.Name == category && s.Id == productId)
					.Include(s => s.ProductToProperty)
					.ThenInclude(sp => sp.ProductProperty)
					.ThenInclude(b => b.ProductBaseProperty)
					.FirstOrDefaultAsync();

			return product;
		}

		public async Task<Product> GetProduct(string category, string stringUrlName)
		{
			Product product = await _context.Products
				.Where(s => s.Category.Name == category && s.UrlName == stringUrlName)
					.Include(s => s.ProductToProperty)
					.ThenInclude(sp => sp.ProductProperty)
					.ThenInclude(b => b.ProductBaseProperty)
					.FirstOrDefaultAsync();

			return product;
		}

		public async Task<IList<Product>> GetProducts()
		{
			IList<Product> products = await _context.Products.ToArrayAsync();

			return products;
		}

		public async Task<bool> UpdateProduct(Product smartPhone)
		{
			throw new NotImplementedException();
		}
	}
}
