using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace WebEngine.Bot
{
	public class UrlParser
	{

		public IList<string> GetUrls(string baseUrl, int pageCount)
		{
			IList<string> urls = new List<string>();

			for (int i = pageCount; i > 0; i--)
			{
				string url = baseUrl + "&page=" + i;

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

				request.Method = "GET";
				request.Accept = "application/json";

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				StringBuilder output = new StringBuilder();
				output.Append(reader.ReadToEnd());

				response.Close();

				string result = output.ToString();

				JObject obj = JObject.Parse(result);

				JArray products = (JArray)obj["products"];

				int productsCount = products.Count;

				for (int index = 0; index < productsCount; index++)
				{
					urls.Add(products[index]["html_url"].ToString());
				}

				Thread.Sleep(1000);
			}

			return urls;
		}
	}
}
