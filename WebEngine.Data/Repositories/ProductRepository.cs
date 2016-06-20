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
	using WebEngine.Core.PageModels;
	using WebEngine.Core.Static;

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

		public async Task<ProductPage> GetProductPage(ProductFilter productFilter)
		{
			if (productFilter != null && productFilter.CategoryId > DEFAULT_ID)
			{
				ProductPage page = new ProductPage();

				ProductSqlQueries sql = GetSqlQueries(productFilter);

				page.Products = await GetProductsAsync(sql.ProductSqlQuery, sql.ProductParameters);

				int count = await GetTotalPages(sql.CountSqlQuery, sql.CountParameters);

				page.TotalPages = count % productFilter.PageSize == DEFAULT_ID ? count / productFilter.PageSize : count / productFilter.PageSize + 1;

				return page;
			}

			return null;
		}

		#endregion

		#region Private methods

		private ProductSqlQueries GetSqlQueries(ProductFilter filter)
		{
			ProductSqlQueries sql = new ProductSqlQueries();

			int propertiesCount = filter.Properties != null ? filter.Properties.Count : DEFAULT_ID;

			StringBuilder sb = new StringBuilder();

			sb.Append("(");

			int paramIndex = 0;

			sql.ProductParameters = new SqlParameter[propertiesCount * 2 + 2];
			sql.CountParameters = new SqlParameter[propertiesCount * 2];

			if (propertiesCount > DEFAULT_ID)
			{
				int lastIndex = propertiesCount - 1;

				for (int i = 0; i < propertiesCount; i++)
				{
					if (filter.Properties[i].IsRange)
					{
					}
					else
					{
						sql.ProductParameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].PropertyId,
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Input
						};

						sql.CountParameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].PropertyId,
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Input
						};

						sb.Append(QueryStrings.ProductQuery);
						sb.Append($" WHERE pp.PropertyId = @param{paramIndex} AND p.CategoryId = {filter.CategoryId}");
						paramIndex++;

						sql.ProductParameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].Value,
							SqlDbType = SqlDbType.NVarChar,
							Direction = ParameterDirection.Input
						};

						sql.CountParameters[paramIndex] = new SqlParameter()
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
						sb.Append(QueryStrings.Intersect);
					}
				}
			}
			else
			{
				sb.Append(QueryStrings.ProductsWithoutProperties);
				sb.Append($" WHERE p.CategoryId = {filter.CategoryId}");
			}

			sql.CountSqlQuery =$"select count(result.Id) as ProductCount from " + sb.ToString() +") as result";

			sql.ProductParameters[paramIndex] = new SqlParameter($"@param{paramIndex}", filter.CurrentPage);

			paramIndex++;

			sql.ProductParameters[paramIndex] = new SqlParameter($"@param{paramIndex}", filter.PageSize);

			sb.Append($") ORDER BY p.Id OFFSET ((@param{paramIndex - 1} - 1) * @param{paramIndex}) ROWS FETCH NEXT @param{paramIndex} ROWS ONLY;");

			sql.ProductSqlQuery = sb.ToString();

			return sql;
		}

		private async Task<IList<Product>> GetProductsAsync(string query, SqlParameter[] parameters)
		{
			List<Product> products = new List<Product>();

			using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = sqlConnection.CreateCommand())
				{
					cmd.CommandText = query;
					cmd.Parameters.AddRange(parameters);
					cmd.CommandTimeout = 30;
					cmd.CommandType = CommandType.Text;

					try
					{
						await sqlConnection.OpenAsync();

						SqlDataReader reader = cmd.ExecuteReader();

						while (await reader.ReadAsync())
						{
							products.Add(new Product()
							{
								Id = (int)reader["Id"],
								Name = (string)reader["Name"],
								ShortInfo = (string)reader["ShortInfo"],
								Company = new Company()
								{
									Name = (string)reader["CompanyName"]
								},
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

			return products;
		}


		private async Task<int> GetTotalPages(string query, SqlParameter[] parameters)
		{
			using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = sqlConnection.CreateCommand())
				{
					cmd.CommandText = query;

					if (parameters.Count() > DEFAULT_ID)
					{
						cmd.Parameters.AddRange(parameters);
					}

					cmd.CommandTimeout = 30;
					cmd.CommandType = CommandType.Text;

					try
					{
						await sqlConnection.OpenAsync();

						SqlDataReader reader = cmd.ExecuteReader();

						await reader.ReadAsync();

						int count = (int)reader[0];

						reader.Dispose();

						return count;
					}
					catch
					{
						return DEFAULT_ID;
					}
				}
			}
		}

		#endregion
	}

	internal class ProductSqlQueries
	{
		public SqlParameter[] ProductParameters { get; set; }

		public SqlParameter[] CountParameters { get; set; }

		public string ProductSqlQuery { get; set; }

		public string CountSqlQuery { get; set; }
	}
}
