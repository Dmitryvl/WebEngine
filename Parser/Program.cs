
namespace Parser
{

	#region Usings

	using Microsoft.EntityFrameworkCore;
	using Models;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using WebEngine.Core.Entities;
	using WebEngine.Data;

	#endregion

	public class Program
	{
		public static void Main(string[] args)
		{
			UrlParser urlParser = new UrlParser();
			ProductParser productParser = new ProductParser();

			string baseUrl = "#";

			IList<ProductModel> products = urlParser.GetProducts(baseUrl, 66);

			DbContextOptionsBuilder<WebEngineContext> options = new DbContextOptionsBuilder<WebEngineContext>();
			options.UseSqlServer("Server=(localdb)\\projectsv12;Database=WebEngine11;Trusted_Connection=True;MultipleActiveResultSets=true");

			WebEngineContext context = new WebEngineContext(options.Options);

			foreach (var pr in products)
			{
				ProductModel product = Task.Run(() => productParser.GetProduct(pr)).Result;

				Product p = new Product()
				{
					CategoryId = 1,
					IsActive = true,
					Name = product.Name,
					ShortInfo = product.ShortInfo,
					UrlName = product.UrlKey
				};

				context.Products.Add(p);
				context.SaveChanges();

				foreach (var header in product.Table.Headers)
				{
					BaseProperty bp = context.BaseProperties.FirstOrDefault(b => b.Name == header.Name);

					if (bp == null)
					{
						bp = new BaseProperty() { IsActive = true, Name = header.Name };

						context.BaseProperties.Add(bp);

						context.SaveChanges();
					}

					IList<Row> rows = product.Table.Rows.Where(r => r.RowHeaderId == bp.Id).ToArray();

					foreach (var row in rows)
					{
						Property prop = context.Properties.FirstOrDefault(pp => pp.Name == row.LeftValue);

						if (prop == null)
						{
							prop = new Property()
							{
								IsActive = true,
								BasePropertyId = bp.Id,
								DataTypeId = 1,
								Name = row.LeftValue,
							};

							context.Properties.Add(prop);
							context.SaveChanges();
						}

						ProductToProperty ppp = new ProductToProperty();
						ppp.ProductId = p.Id;
						ppp.PropertyId = prop.Id;
						ppp.SizeValue = "";
						ppp.Value = row.RightValue == string.Empty ? row.IsExist.ToString() : row.RightValue;

						context.ProductToProperty.Add(ppp);
						context.SaveChanges();
					}
				}
			}
			Console.WriteLine("complited");
			Console.ReadKey();
		}
	}
}
