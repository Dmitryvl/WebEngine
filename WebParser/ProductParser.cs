using AngleSharp;
using AngleSharp.Dom;
using System.Threading.Tasks;
using WebEngine.Bot.Models;

namespace WebEngine.Bot
{
	public class ProductParser
	{
		private const string _tableSelector = "table.product-specs__table tbody";

		private const string _trSelector = "tr";

		private const string _tdSelector = "td";

		private const string _spanIsExist = "span.i-tip";

		private const string _spanIsNotExist = "span.i-x";

		private const string _productTitle = "h2.catalog-masthead__title";

		public async Task<ProductModel> GetProduct(string url)
		{
			if (!string.IsNullOrEmpty(url))
			{
				ProductModel product = new ProductModel();

				IConfiguration config = Configuration.Default.WithDefaultLoader();

				IDocument document = await BrowsingContext.New(config).OpenAsync(url);

				IElement name = document.QuerySelector(_productTitle);

				product.Name = name.TextContent.Trim();

				IHtmlCollection<IElement> tbodies = document.QuerySelectorAll(_tableSelector);

				int headerId = 0;

				foreach (var tag in tbodies)
				{
					var trs = tag.QuerySelectorAll(_trSelector);

					foreach (var tr in trs)
					{
						var tds = tr.QuerySelectorAll(_tdSelector);

						for (int i = 0; i < tds.Length; i += 2)
						{
							if (i == 0 && i == tds.Length - 1)
							{
								string temp = (tds[i].TextContent).Trim();

								int index = temp.IndexOf('\n');

								headerId++;

								RowHeader header = new RowHeader();
								header.Id = headerId;
								header.Name = index > 0 ? temp.Substring(0, index) : temp;

								product.Table.Headers.Add(header);
							}
							else
							{
								Row row = new Row();
								row.RowHeaderId = headerId;

								IHtmlCollection<IElement> span = tds[i + 1].QuerySelectorAll(_spanIsExist);

								row.IsExist = span.Length > 0 ? true : false;

								string temp1 = (tds[i].TextContent).Trim();
								string temp2 = (tds[i + 1].TextContent).Trim();

								int index1 = temp1.IndexOf('\n');
								int index2 = temp2.IndexOf('\n');

								row.LeftValue = index1 > 0 ? temp1.Substring(0, index1) : temp1;
								row.RightValue = index2 > 0 ? temp2.Substring(0, index2) : temp2;

								product.Table.Rows.Add(row);
							}
						}
					}
				}

				return product;
			}

			return null;
		}
	}
}
