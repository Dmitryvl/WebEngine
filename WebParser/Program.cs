
namespace WebEngine.Bot
{
	#region Usings

	using Models;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class Program
	{
		public static void Main(string[] args)
		{
			UrlParser urlParser = new UrlParser();
			ProductParser productParser = new ProductParser();

			string url = "http://catalog.onliner.by/mobile/motorola/motoxplay16b";

			string baseUrl = "http://catalog.onliner.by/mobile?page=1";

			IList<string> urls = Task.Run(() => urlParser.GetUrls(baseUrl, 1)).Result;


			//ProductModel product = Task.Run(() => productParser.GetProduct(url)).Result;

			//Console.WriteLine(product.Name);

			//foreach (RowHeader header in product.Table.Headers)
			//{
			//	Console.WriteLine(header.Name);

			//	IList<Row> rows = product.Table.Rows.Where(r => r.RowHeaderId == header.Id).ToArray();

			//	foreach (Row row in rows)
			//	{
			//		Console.WriteLine(row.LeftValue + " | " + row.RightValue + " | " + row.IsExist);
			//	}
			//}

			Console.ReadKey();
		}
	}
}
