// -----------------------------------------------------------------------
// <copyright file="IProductPageRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Core.Interfaces
{
	#region Usings

	using System.Threading.Tasks;
	using WebEngine.Core.PageModels;
	using WebEngine.Core.Filters;

	#endregion

	/// <summary>
	/// <see cref="IProductPageRepository"/> interface.
	/// </summary>
	public interface IProductPageRepository
	{
		Task<ProductPage> GetProductPage(ProductFilter productFilter);
	}
}
