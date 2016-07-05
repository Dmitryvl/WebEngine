namespace Parser.Models
{
	public class ProductModel
	{
		public string Name { get; set; }

		public Table Table { get; set; }

		public ProductModel()
		{
			Table = new Table();
		}
	}
}
