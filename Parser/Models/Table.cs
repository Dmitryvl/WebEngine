using System.Collections.Generic;

namespace Parser.Models
{
	public class Table
	{
		public IList<Row> Rows { get; set; }

		public IList<RowHeader> Headers { get; set; }

		public Table()
		{
			Rows = new List<Row>();
			Headers = new List<RowHeader>();
		}
	}
}
