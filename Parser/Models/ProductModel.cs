namespace Parser.Models
{
	public class ProductModel
	{
		public string Name { get; set; }

		public string Url { get; set; }

		public string UrlKey { get; set; }

		public string ShortInfo { get; set; }

		public Table Table { get; set; }

		public ProductModel()
		{
			Table = new Table();
		}
	}
}
