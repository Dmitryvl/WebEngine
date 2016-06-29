
namespace WebEngine.Bot
{
	#region Usings

	using AngleSharp;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	#endregion

	public class Program
	{
		public static void Main(string[] args)
		{
			var config = Configuration.Default.WithDefaultLoader();

			var address = "#";

			var document = Task.Run(() => BrowsingContext.New(config).OpenAsync(address)).Result;

			var cellSelector = "table.product-specs__table tbody";

			var cells = document.QuerySelectorAll(cellSelector);

			List<Item> items = new List<Item>();

			string s1 = "tr";
			string s2 = "td";

			foreach (var tag in cells)
			{
				var trs = tag.QuerySelectorAll(s1);

				foreach (var tr in trs)
				{
					var tds = tr.QuerySelectorAll(s2);

					for (int i = 0; i < tds.Length; i+=2)
					{
						if (i == 0 && i == tds.Length - 1)
						{
							string temp = (tds[i].TextContent).Trim();

							int index = temp.IndexOf('\n');

							Item item = new Item();

							item.Prop1 = index > 0 ? temp.Substring(0, index) : temp;

							item.Prop2 = "head";

							items.Add(item);
						}
						else
						{
							Item itm = new Item();

							var span1 = tds[i+1].QuerySelectorAll("span.i-tip");
							var span2 = tds[i+1].QuerySelectorAll("span.i-x");

							if (span1.Length > 0)
							{
								itm.Flag = true;
							}
							if (span2.Length > 0)
							{
								itm.Flag = false;
							}

							string temp1 = (tds[i].TextContent).Trim();
							string temp2 = (tds[i+1].TextContent).Trim();

							int index1 = temp1.IndexOf('\n');
							int index2 = temp2.IndexOf('\n');

							itm.Prop1 = index1 > 0 ? temp1.Substring(0, index1) : temp1;
							itm.Prop2 = index2 > 0 ? temp2.Substring(0, index2) : temp2;

							items.Add(itm);
						}
					}
				}
			}

			System.Console.ReadKey();
		}
	}

	class Item
	{
		public int HeaderId { get; set; }

		public string Prop1 { get; set; }

		public string Prop2 { get; set; }

		public bool Flag { get; set; }
	}
}
