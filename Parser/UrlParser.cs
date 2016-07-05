using Newtonsoft.Json.Linq;
using Parser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser
{
	public class UrlParser
	{

		public IList<ProductModel> GetProducts(string baseUrl, int pageCount)
		{
			IList<ProductModel> prod = new List<ProductModel>();

			for (int i = pageCount; i > 0; i--)
			{
				string url = baseUrl + "&page=" + i;

				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Clear();

					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage res = Task.Run(() => client.GetAsync(url)).Result;

					string result = Task.Run(() => res.Content.ReadAsStringAsync()).Result;


					JObject obj = JObject.Parse(result);

					JArray products = (JArray)obj["products"];

					int productsCount = products.Count;

					for (int index = 0; index < productsCount; index++)
					{
						prod.Add(new ProductModel()
						{
							Name = products[index]["full_name"].ToString(),
							Url = products[index]["html_url"].ToString(),
							UrlKey = products[index]["key"].ToString(),
							ShortInfo = products[index]["description"].ToString(),
						});
					}
				}

				Thread.Sleep(1000);
			}

			return prod;
		}
	}
}
