// -----------------------------------------------------------------------
// <copyright file="ProductPageRepository.cs" author="Dzmitry Prakapenka">
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
	using System.Text;
	using System.Threading.Tasks;

	using Microsoft.Extensions.Options;

	using WebEngine.Core.Config;
	using WebEngine.Core.Entities;
	using WebEngine.Core.Filters;
	using WebEngine.Core.Interfaces;
	using WebEngine.Core.PageModels;
	using WebEngine.Core.Static;

	#endregion

	/// <summary>
	/// <see cref="ProductPageRepository"/> class.
	/// </summary>
	public class ProductPageRepository : BaseRepository, IProductPageRepository
	{
		#region Constructors

		public ProductPageRepository(IServiceProvider services, IOptions<AppConfig> config) : base(services, config)
		{
		}

		#endregion

		#region Public methods

		public async Task<ProductPage> GetProductPage(ProductFilter productFilter)
		{
			if (productFilter != null && productFilter.CategoryId > DEFAULT_ID)
			{
				Query query = BuildQuery(productFilter);

				ProductPage page = await GetProductPageAsync(query.QueryString, query.Parameters);

				page.TotalPages = page.ProductCount % productFilter.PageSize == DEFAULT_ID
					? page.ProductCount / productFilter.PageSize
					: page.ProductCount / productFilter.PageSize + 1;

				return page;
			}

			return null;
		}

		#endregion

		#region Private methods

		private Query BuildQuery(ProductFilter filter)
		{
			Query query = new Query();

			int propertiesCount = filter.Properties != null ? filter.Properties.Count : DEFAULT_ID;

			StringBuilder sb = new StringBuilder();

			sb.Append("(");

			int paramIndex = 0;

			query.Parameters = new SqlParameter[propertiesCount * 2 + 2];

			if (propertiesCount > DEFAULT_ID)
			{
				int lastIndex = propertiesCount - 1;

				for (int i = 0; i < propertiesCount; i++)
				{
					if (filter.Properties[i].IsRange)
					{
						query.Parameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].PropertyId,
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Input
						};

						sb.Append(QueryStrings.ProductQuery);

						sb.Append($" WHERE pp.PropertyId = @param{paramIndex} AND p.CategoryId = {filter.CategoryId}");

						paramIndex++;

						query.Parameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].Value,
							SqlDbType = SqlDbType.Float,
							Direction = ParameterDirection.Input
						};

						if (filter.Properties[i].Operation == '<')
						{
							sb.Append($" AND @param{paramIndex} >= Convert(float, pp.Value)");
						}
						else if (filter.Properties[i].Operation == '>')
						{
							sb.Append($" AND @param{paramIndex} <= Convert(float, pp.Value)");
						}

						paramIndex++;
					}
					else
					{
						query.Parameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].PropertyId,
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Input
						};

						sb.Append(QueryStrings.ProductQuery);
						sb.Append($" WHERE pp.PropertyId = @param{paramIndex} AND p.CategoryId = {filter.CategoryId}");
						paramIndex++;

						query.Parameters[paramIndex] = new SqlParameter()
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

			string countQueryString = $"SELECT COUNT(result.Id) AS ProductCount FROM " + sb.ToString() + ") AS result";

			query.Parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", filter.CurrentPage);

			paramIndex++;

			query.Parameters[paramIndex] = new SqlParameter($"@param{paramIndex}", filter.PageSize);

			sb.Append($") ORDER BY p.Id OFFSET ((@param{paramIndex - 1} - 1) * @param{paramIndex}) ROWS FETCH NEXT @param{paramIndex} ROWS ONLY;");

			sb.Append(countQueryString);

			query.QueryString = sb.ToString();

			return query;
		}

		private async Task<ProductPage> GetProductPageAsync(string query, SqlParameter[] parameters)
		{
			ProductPage page = new ProductPage();

			page.Products = new List<Product>();

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

						if (reader.HasRows)
						{
							while (await reader.ReadAsync())
							{
								page.Products.Add(new Product()
								{
									Id = (int)reader["Id"],
									Name = (string)reader["Name"],
									ShortInfo = (string)reader["ShortInfo"],
									UrlName = (string)reader["UrlName"],
									Category = new Category()
									{
										Name = (string)reader["CategoryName"]
									}
								});
							}

							await reader.NextResultAsync();

							await reader.ReadAsync();

							page.ProductCount = (int)reader["ProductCount"];
						}

						reader.Dispose();
					}
					catch (Exception ex)
					{
						return null;
					}
				}
			}

			return page;
		}

		#endregion
	}

	internal struct Query
	{
		public SqlParameter[] Parameters { get; set; }

		public string QueryString { get; set; }
	}
}
