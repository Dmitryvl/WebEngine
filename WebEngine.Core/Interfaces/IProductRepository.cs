// -----------------------------------------------------------------------
// <copyright file="ISmartPhoneRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using WebEngine.Core.Entities;
	using WebEngine.Core.Filters;

	#endregion

	/// <summary>
	/// <see cref="IProductRepository"/> interface.
	/// </summary>
	public interface IProductRepository : IDisposable
	{
		/// <summary>
		/// Get products by category name.
		/// </summary>
		/// <param name="category">Category name.</param>
		/// <returns>Return product collection.</returns>
		//Task<IList<Product>> GetProductsAsync(string category);

		/// <summary>
		/// Get filtered products.
		/// </summary>
		/// <param name="filter">Product filter.</param>
		/// <returns>Return product collection.</returns>
		Task<IList<Product>> GetProductsAsync(ProductFilter filter);

		/// <summary>
		/// Get filtered products.
		/// </summary>
		/// <param name="categoryId">Category id.</param>
		/// <param name="currentPage">Current page number.</param>
		/// <param name="pageSize">Page size</param>
		/// <returns>Return product collection.</returns>
		Task<IList<Product>> GetProductsAsync(int categoryId, int currentPage, int pageSize);

		/// <summary>
		/// Get product by product id.
		/// </summary>
		/// <param name="productId">Product id.</param>
		/// <returns>Return product.</returns>
		Task<Product> GetProductAsync(int productId);

		/// <summary>
		/// Get product by product category.
		/// </summary>
		/// <param name="category">Category name.</param>
		/// <param name="stringUrlName">Url name.</param>
		/// <returns>Return product.</returns>
		Task<Product> GetProductAsync(string category, string stringUrlName);

		/// <summary>
		/// Add product.
		/// </summary>
		/// <param name="product">New product.</param>
		/// <returns>Return result.</returns>
		Task<bool> AddProductAsync(Product product);

		/// <summary>
		/// Update product.
		/// </summary>
		/// <param name="product">Changed product.</param>
		/// <returns>Return result.</returns>
		Task<bool> UpdateProductAsync(Product product);

		/// <summary>
		/// Delete product.
		/// </summary>
		/// <param name="productId">Product id.</param>
		/// <returns>Return result.</returns>
		Task<bool> DeleteProductAsync(int productId);
	}
}
