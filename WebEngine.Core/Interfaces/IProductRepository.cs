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

	#endregion

	/// <summary>
	/// <see cref="IProductRepository"/> interface.
	/// </summary>
	public interface IProductRepository : IDisposable
	{
		Task<IList<Product>> GetProducts();

		Task<Product> GetProduct(int productId);

		Task<Product> GetProduct(string category, string stringUrlName);

		Task<Product> GetProduct(string category, int productId);

		Task<bool> AddProduct(Product product);

		Task<bool> UpdateProduct(Product product);

		Task<bool> DeleteProduct(int productId);
	}
}
