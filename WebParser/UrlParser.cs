
using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebEngine.Bot
{
	public class UrlParser
	{
		public const string _productsSelector = "div#schema-products div.schema-product__group";

		public async Task<IList<string>> GetUrls(string baseUrl, int pageCount)
		{
			IList<string> urls = new List<string>();

			IConfiguration config = Configuration.Default.WithJavaScript().WithCss();

			IDocument document = await BrowsingContext.New(config).OpenAsync(baseUrl);

			Console.WriteLine(document.DocumentElement.OuterHtml);

			IHtmlCollection<IElement> products = document.QuerySelectorAll(_productsSelector);



			return urls;
		}
	}
}
