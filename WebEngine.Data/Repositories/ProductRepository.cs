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
					.FirstOrDefaultAsync();

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
					.FirstOrDefaultAsync();

				if (product != null)
				{
					product.ProductToProperty = await _context.ProductToProperty
						.Where(pp => pp.ProductId == product.Id && pp.Property.IsActive == true && pp.Property.BaseProperty.IsActive == true)
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
						.ToArrayAsync();
				}

				return product;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Get products by canegory name.
		/// </summary>
		/// <param name="category">Category name.</param>
		/// <returns>Return product collection.</returns>
		public async Task<IList<Product>> GetProductsAsync(string category)
		{
			if (!string.IsNullOrEmpty(category))
			{
				Product[] products = await _context.Products
					.Where(p => p.Category.Name == category)
					.Select(p => new Product()
					{
						Category = p.Category,
						Id = p.Id,
						Name = p.Name,
						Company = p.Company,
						ShortInfo = p.ShortInfo,
						UrlName = p.UrlName
					}).ToArrayAsync();

				return products;
			}

			return null;
		}

		/// <summary>
		/// Get filtred products.
		/// </summary>
		/// <param name="filter">Product filter.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="currentPage">Current page.</param>
		/// <returns>Return product collection.</returns>
		public async Task<IList<Product>> GetProductsAsync(ProductFilter filter, int pageSize, int currentPage)
		{
			if (filter != null)
			{
				StringBuilder sb = new StringBuilder();

				//sb.Append("(");

				int length = filter.Properties.Count;

				int paramIndex = 0;

				SqlParameter[] parameters = new SqlParameter[filter.Properties.Count * 2];

				string intersect = " INTERSECT ";

				int lastIndex = length - 1;

				for (int i = 0; i < length; i++)
				{
					if (filter.Properties[i].IsRange /*&& filter.Properties[0].RangeId > DEFAULT_ID*/)
					{
						parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", int.Parse(filter.Properties[i].Value));

						sb.Append("SELECT p.Id, p.Name FROM ProductToProperty as pp INNER JOIN Products as p on pp.ProductId = p.Id");
						sb.Append($" WHERE pp.PropertyId {filter.Properties[i].Operation} @param{paramIndex}");
						paramIndex++;
						//sb.Append($" AND pp.Value = @param{paramIndex}");
						//paramIndex++;
					}
					//else if (filter.Properties[0].IsRange)
					//{

					//}
					else
					{
						parameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].PropertyId,
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Input
						};

						sb.Append("SELECT p.Id, p.Name FROM ProductToProperty as pp INNER JOIN Products as p on pp.ProductId = p.Id");
						sb.Append($" WHERE pp.PropertyId = @param{paramIndex}");
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

				//parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", currentPage);

				string sql = sb.ToString();

				string cs = "Server=(localdb)\\projectsv12;Database=WebEngine11;Trusted_Connection=True;MultipleActiveResultSets=true";

				List<Product> prods = new List<Product>();

				using (SqlConnection sqlConnection = new SqlConnection(cs))
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
									Name = (string)reader["Name"]
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

				//sb.Append(") ORDER BY p.Id OFFSET ((@CurrentPage - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;");

				return prods;
			}

			return null;
		}

		#endregion
	}
}
