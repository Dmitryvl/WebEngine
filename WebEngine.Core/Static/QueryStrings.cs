﻿// -----------------------------------------------------------------------
// <copyright file="ProductPage.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Static
{
	public static class QueryStrings
	{
		public static string ProductQuery = "SELECT p.Id, p.Name, p.ShortInfo, p.UrlName, c.Name as CategoryName, com.Name as CompanyName FROM ProductToProperty as pp INNER JOIN Products as p on pp.ProductId = p.Id INNER JOIN Categories as c on p.CategoryId = c.Id INNER JOIN Companies as com on p.CompanyId = com.Id";

		public static string ProductsWithoutProperties = "SELECT p.Id, p.Name, p.ShortInfo, p.UrlName, c.Name as CategoryName, com.Name as CompanyName FROM Products as p INNER JOIN Categories as c on p.CategoryId = c.Id INNER JOIN Companies as com on p.CompanyId = com.Id";

		public static string Intersect = " INTERSECT ";
	}
}
