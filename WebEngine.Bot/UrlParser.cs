
using AngleSharp;
using AngleSharp.Dom;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebEngine.Bot
{
	public class UrlParser
	{
		public const string _productsSelector = "div.schema-product__title a";

		public async Task<IList<string>> GetUrls(string baseUrl, int pageCount)
		{
			IList<string> urls = new List<string>();

			IConfiguration config = Configuration.Default.WithJavaScript();

			IDocument document = await BrowsingContext.New(config).OpenAsync(baseUrl);

			IHtmlCollection<IElement> products = document.QuerySelectorAll(_productsSelector);



			return urls;
		}
	}
}
