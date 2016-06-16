// -----------------------------------------------------------------------
// <copyright file="ProductRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;

	using WebEngine.Core.Config;
	using WebEngine.Core.Entities;
	using WebEngine.Core.Filters;
	using WebEngine.Core.Interfaces;
	using Core.PageModels;
	using System.Collections;
	#endregion

	/// <summary>
	/// <see cref="ProductRepository"/> class.
	/// </summary>
	public class ProductRepository : BaseRepository, IProductRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public ProductRepository(IServiceProvider services, IOptions<AppConfig> config) : base(services, config)
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
					.Include(s => s.Company)
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
						CompanyId = GetValue(p.CompanyId),
						ShortInfo = GetValue(p.ShortInfo),
						UrlName = GetValue(p.UrlName),
						Company = new Company()
						{
							Id = GetValue(p.Company.Id),
							Name = GetValue(p.Company.Name)
						}
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

		/// <summary>
		/// Get filtred products.
		/// </summary>
		/// <param name="filter">Product filter.</param>
		/// <returns>Return product collection.</returns>
		public async Task<IList<Product>> GetProductsAsync(ProductFilter filter)
		{
			if (filter != null && filter.CategoryId > DEFAULT_ID)
			{
				int propertiesCount = filter.Properties != null ? filter.Properties.Count : DEFAULT_ID;

				string query = "SELECT p.Id, p.Name, p.ShortInfo, c.Name as CategoryName FROM ProductToProperty as pp INNER JOIN Products as p on pp.ProductId = p.Id INNER JOIN Categories as c on p.CategoryId = c.Id";

				if (propertiesCount > DEFAULT_ID)
				{
					StringBuilder sb = new StringBuilder();

					sb.Append("(");

					int paramIndex = 0;

					SqlParameter[] parameters = new SqlParameter[filter.Properties.Count * 2 + 2];

					string intersect = " INTERSECT ";

					int lastIndex = propertiesCount - 1;

					for (int i = 0; i < propertiesCount; i++)
					{
						if (filter.Properties[i].IsRange /*&& filter.Properties[0].RangeId > DEFAULT_ID*/)
						{
							parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", int.Parse(filter.Properties[i].Value));

							sb.Append(query);
							sb.Append($" WHERE pp.PropertyId {filter.Properties[i].Operation} @param{paramIndex}");
							paramIndex++;
							//sb.Append($" AND pp.Value = @param{paramIndex}");
							//paramIndex++;
						}
						else
						{
							parameters[paramIndex] = new SqlParameter()
							{
								ParameterName = $"@param{paramIndex}",
								Value = filter.Properties[i].PropertyId,
								SqlDbType = SqlDbType.Int,
								Direction = ParameterDirection.Input
							};

							sb.Append(query);
							sb.Append($" WHERE pp.PropertyId = @param{paramIndex} AND p.CategoryId = {filter.CategoryId}");
							paramIndex++;

							parameters[paramIndex] = new SqlParameter()
							{
								ParameterName = $"@param{paramIndex}",
								Value = filter.Properties[i].Value,
								SqlDbType = SqlDbType.NVarChar,
								Direction = ParameterDirection.Input
							};

							sb.Append($" AND pp.Value = @param{paramIndex}");
							paramIndex++;
						}

						if (i != lastIndex)
						{
							sb.Append(intersect);
						}
					}

					parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", filter.CurrentPage);

					paramIndex++;

					parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", filter.PageSize);

					sb.Append($") ORDER BY p.Id OFFSET ((@param{paramIndex - 1} - 1) * @param{paramIndex}) ROWS FETCH NEXT @param{paramIndex} ROWS ONLY;");

					string sql = sb.ToString();

					List<Product> prods = new List<Product>();

					using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
					{
						using (SqlCommand cmd = sqlConnection.CreateCommand())
						{
							cmd.CommandText = sql;
							cmd.Parameters.AddRange(parameters);
							cmd.CommandTimeout = 15;
							cmd.CommandType = CommandType.Text;

							try
							{
								await sqlConnection.OpenAsync();

								SqlDataReader reader = cmd.ExecuteReader();

								while (await reader.ReadAsync())
								{
									prods.Add(new Product()
									{
										Id = (int)reader["Id"],
										Name = (string)reader["Name"],
										ShortInfo = (string)reader["ShortInfo"],
										Category = new Category()
										{
											Name = (string)reader["CategoryName"]
										}
									});
								}

								reader.Dispose();
							}
							catch
							{
								return null;
							}
						}
					}

					return prods;
				}
				else
				{
					return await GetProductsAsync(filter.CategoryId, filter.CurrentPage, filter.PageSize);
				}
			}

			return null;
		}

		/// <summary>
		/// Get filtered products.
		/// </summary>
		/// <param name="categoryId">Category id.</param>
		/// <param name="currentPage">Current page number.</param>
		/// <param name="pageSize">Page size</param>
		/// <returns>Return product collection.</returns>
		public async Task<IList<Product>> GetProductsAsync(int categoryId, int currentPage, int pageSize)
		{
			if (categoryId > DEFAULT_ID)
			{
				try
				{
					Product[] products = await _context.Products
						.Where(p => p.CategoryId == categoryId)
						.Select(p => new Product()
						{
							Id = GetValue(p.Id),
							Name = GetValue(p.Name),
							UrlName = GetValue(p.UrlName),
							ShortInfo = GetValue(p.ShortInfo),
							Company = new Company()
							{
								Id = GetValue(p.Company.Id),
								Name = GetValue(p.Company.Name)
							}
						})
						.OrderByDescending(p => p.Id)
						.Skip((currentPage - 1) * pageSize)
						.Take(pageSize)
						.ToArrayAsync()
						.ConfigureAwait(false);

					return products;
				}
				catch
				{
					return null;
				}
			}

			return null;
		}

		public async Task<ProductPage> GetProductPage(ProductFilter productFilter)
		{
			if (productFilter != null && productFilter.CategoryId > DEFAULT_ID)
			{
				ProductPage page = new ProductPage();

				int propertiesCount = productFilter.Properties != null ? productFilter.Properties.Count() : DEFAULT_ID;

				if (propertiesCount > DEFAULT_ID)
				{

				}
				page.Products = await GetProductsAsync(productFilter);

				int count = await GetProductsCount(productFilter.CategoryId);

				page.TotalPages = count % productFilter.PageSize == DEFAULT_ID ? count / productFilter.PageSize : count / productFilter.PageSize + 1;

				return page;
			}

			return null;
		}

		#endregion

		#region Private methods

		private async Task<int> GetProductsCount(int categoryId)
		{
			if (categoryId > DEFAULT_ID)
			{
				try
				{
					int count = await _context.Products
						.CountAsync(p => p.CategoryId == categoryId)
						.ConfigureAwait(false);

					return count;
				}
				catch
				{
					return DEFAULT_ID;
				}
			}

			return DEFAULT_ID;
		}

/* select count(result.Id) as ProductCount from
(SELECT p.Id, p.Name, p.ShortInfo, c.Name FROM ProductToProperty as pp
INNER JOIN Products as p on pp.ProductId = p.Id
INNER JOIN Categories as c on p.CategoryId = c.Id
WHERE pp.PropertyId = 6 AND p.CategoryId = 1 AND CONVERT(float, pp.Value) > 5.1)
INTERSECT SELECT p.Id, p.Name, p.ShortInfo, c.Name FROM ProductToProperty as pp 
INNER JOIN Products as p on pp.ProductId = p.Id 
INNER JOIN Categories as c on p.CategoryId = c.Id
WHERE pp.PropertyId = 4 AND p.CategoryId = 1 AND pp.Value = '11')
as result */

		#endregion
	}
}
