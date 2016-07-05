
namespace Parser
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

			string baseUrl = "#";

			IList<string> urls = urlParser.GetUrls(baseUrl, 66);



			foreach (var url in urls)
			{
				ProductModel product = Task.Run(() => productParser.GetProduct(url)).Result;


			}

			Console.ReadKey();
		}
	}
}
