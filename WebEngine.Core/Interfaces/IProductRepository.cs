// -----------------------------------------------------------------------
// <copyright file="ISmartPhoneRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	#region Usings

	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Filters;

	#endregion

	/// <summary>
	/// <see cref="IProductRepository"/> interface.
	/// </summary>
	public interface IProductRepository : IDisposable
	{
		Task<IList<Product>> GetProductsAsync(string category);

		Task<IList<Product>> GetProductsAsync(ProductFilter filter);

		Task<Product> GetProductAsync(int productId);

		Task<Product> GetProductAsync(string category, string stringUrlName);

		Task<Product> GetProductAsync(string category, int productId);

		Task<bool> AddProductAsync(Product product);

		Task<bool> UpdateProductAsync(Product product);

		Task<bool> DeleteProductAsync(int productId);
	}
}
