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
	using System.Text;
	using System.Data.SqlClient;
	using System.Data;
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
			//if (productId > DEFAULT_ID)
			//{
			//	Product smartPhone = await _context.Products
			//		.Where(s => s.Id == productId)
			//		.Include(s => s.Company)
			//		.Include(s => s.ProductToProperty)
			//		.ThenInclude(sp => sp.Property)
			//		.ThenInclude(b => b.BaseProperty)
			//		.FirstOrDefaultAsync();

			//	return smartPhone;
			//}

			return null;
		}

		public async Task<Product> GetProductAsync(string category, int productId)
		{
			//Product product = await _context.Products
			//	.Where(s => s.Category.Name == category && s.Id == productId)
			//	.Include(s => s.Company)
			//	.Include(s => s.ProductToProperty)
			//	.ThenInclude(sp => sp.Property)
			//	.ThenInclude(b => b.BaseProperty)
			//	.FirstOrDefaultAsync();

			//return product;

			return null;
		}

		public async Task<Product> GetProductAsync(string category, string stringUrlName)
		{
			//Product product = await _context.Products
			//	.Where(s => s.Category.Name == category && s.UrlName == stringUrlName)
			//	.Include(s => s.Company)
			//	.Include(s => s.ProductToProperty)
			//	.ThenInclude(sp => sp.Property)
			//	.ThenInclude(b => b.BaseProperty)
			//	.FirstOrDefaultAsync();

			//return product;

			return null;
		}

		public async Task<IList<Product>> GetProductsAsync(string category)
		{
			//if (!string.IsNullOrEmpty(category))
			//{
			//	IList<Product> products = await _context.Products
			//		.Where(p => p.Category.Name == category)
			//		.Select(p => new Product()
			//		{
			//			Category = p.Category,
			//			Id = p.Id,
			//			Name = p.Name,
			//			Company = p.Company,
			//			ProductToProperty = p.ProductToProperty
			//			.Where(pp => pp.Property.IsPreview == true)
			//			.Select(pp => new ProductToProperty()
			//			{
			//				Value = pp.Value,
			//				SizeValue = pp.SizeValue
			//			}).ToArray()
			//		}).ToArrayAsync();

			//	return products;
			//}

			return null;
		}

		public async Task<IList<Product>> GetProductsAsync(ProductFilter filter, int pageSize, int currentPage)
		{
			filter.Properties = new PropertyFilter[]
			{
				new PropertyFilter() { PropertyId = 2, Value = "11", IsRange = false },
				new PropertyFilter() { PropertyId = 4, Value = "11", IsRange = false },
			};


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

						sb.Append("SELECT p.Id, p.Name FROM ProductToProperty as pp INNER JOIN Product as p on pp.ProductId = p.Id");
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
							SqlDbType = System.Data.SqlDbType.Int,
							Direction = System.Data.ParameterDirection.Input
						};

						sb.Append("SELECT p.Id, p.Name FROM ProductToProperty as pp INNER JOIN Product as p on pp.ProductId = p.Id");
						sb.Append($" WHERE pp.PropertyId = @param{paramIndex}");
						paramIndex++;

						parameters[paramIndex] = new SqlParameter()
						{
							ParameterName = $"@param{paramIndex}",
							Value = filter.Properties[i].Value,
							SqlDbType = System.Data.SqlDbType.NVarChar,
							Direction = System.Data.ParameterDirection.Input
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

				string cs = "Server=(localdb)\\projectsv12;Database=WebEngine10;Trusted_Connection=True;MultipleActiveResultSets=true";

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

							reader.Close();
						}
						catch
						{
							return null;
						}
					}
				}



				//sb.Append(") ORDER BY p.Id OFFSET ((@CurrentPage - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;");

				//var result = _context.Products.FromSql("Select * from Product as p where p.Id = @param0", parameters[0]).ToArray();

				return new List<Product>();
			}

			return null;
		}


	}
}
