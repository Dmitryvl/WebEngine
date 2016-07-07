// -----------------------------------------------------------------------
// <copyright file="ProductRepository.cs" author="Dzmitry Prakapenka">
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

	using WebEngine.Core.Entities;
	using WebEngine.Core.Interfaces;

	#endregion

	/// <summary>
	/// <see cref="ProductRepository"/> class.
	/// </summary>
	public class ProductRepository : BaseRepository<ProductRepository>, IProductRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public ProductRepository(IServiceProvider services) : base(services)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add product.
		/// </summary>
		/// <param name="product">New product.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> AddProductAsync(Product product)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update product.
		/// </summary>
		/// <param name="product">Changed product.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> UpdateProductAsync(Product smartPhone)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete product.
		/// </summary>
		/// <param name="productId">Product id.</param>
		/// <returns>Return result.</returns>
		public async Task<bool> DeleteProductAsync(int productId)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get product by product id.
		/// </summary>
		/// <param name="productId">Product id.</param>
		/// <returns>Return product.</returns>
		public async Task<Product> GetProductAsync(int productId)
		{
			if (productId > DEFAULT_ID)
			{
				Product smartPhone = await _context.Products
					.Where(s => s.Id == productId)
					.Include(s => s.ProductToProperty)
					.ThenInclude(sp => sp.Property)
					.ThenInclude(b => b.BaseProperty)
					.FirstOrDefaultAsync()
					.ConfigureAwait(false);

				return smartPhone;
			}

			return null;
		}

		/// <summary>
		/// Get product by product categoty.
		/// </summary>
		/// <param name="category">Category name.</param>
		/// <param name="urlName">Url name.</param>
		/// <returns>Return product.</returns>
		public async Task<Product> GetProductAsync(string category, string urlName)
		{
			try
			{
				Product product = await _context.Products
					.Where(p => p.Category.Name == category && p.UrlName == urlName && p.IsActive == true)
					.Select(p => new Product()
					{
						Id = GetValue(p.Id),
						Name = GetValue(p.Name),
						CategoryId = GetValue(p.CategoryId),
						ShortInfo = GetValue(p.ShortInfo),
						UrlName = GetValue(p.UrlName),
					})
					.FirstOrDefaultAsync()
					.ConfigureAwait(false);

				if (product != null)
				{
					product.ProductToProperty = await _context.ProductToProperty
						.Where(pp => pp.ProductId == product.Id
						&& pp.Property.IsActive == true
						&& pp.Property.BaseProperty.IsActive == true)
						.Select(pp => new ProductToProperty()
						{
							ProductId = GetValue(pp.ProductId),
							PropertyId = GetValue(pp.PropertyId),
							Value = GetValue(pp.Value),
							SizeValue = GetValue(pp.SizeValue),
							Property = new Property()
							{
								Id = GetValue(pp.Property.Id),
								Name = GetValue(pp.Property.Name),
								DataTypeId = GetValue(pp.Property.DataTypeId),
								DataType = new DataType()
								{
									Id = GetValue(pp.Property.DataType.Id),
									Name = GetValue(pp.Property.DataType.Name),
								},
								BasePropertyId = GetValue(pp.Property.BasePropertyId),
								BaseProperty = new BaseProperty()
								{
									Id = GetValue(pp.Property.BaseProperty.Id),
									Name = GetValue(pp.Property.BaseProperty.Name)
								}
							}
						})
						.ToArrayAsync()
						.ConfigureAwait(false);
				}

				return product;
			}
			catch
			{
				return null;
			}
		}

		#endregion
	}
}
