// -----------------------------------------------------------------------
// <copyright file="ProductFilterRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Core.Entities;
	using Core.Interfaces;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	#endregion

	/// <summary>
	/// <see cref="ProductFilterRepository"/> class.
	/// </summary>
	public class ProductFilterRepository : BaseRepository<ProductFilterRepository>, IProductFilterRepository
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductFilterRepository" /> class.
		/// </summary>
		/// <param name="services">IServiceProvider services.</param>
		/// <param name="config">Application config.</param>
		public ProductFilterRepository(IServiceProvider services) : base(services)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get product filter items.
		/// </summary>
		/// <param name="categoryId">Category id.</param>
		/// <returns>Return filter item collection.</returns>
		public async Task<IList<ProductFilterItem>> GetProductFilterItems(int categoryId)
		{
			if (categoryId > DEFAULT_ID)
			{
				try
				{
					IList<ProductFilterItem> items = await _context.ProductFilterItems
						.Where(p => p.CategoryId == categoryId)
						.Select(p => new ProductFilterItem()
						{
							Id = GetValue(p.Id),
							CategoryId = GetValue(p.CategoryId),
							FilterItemType = GetValue(p.FilterItemType),
							PropertyId = GetValue(p.PropertyId),
							Property = new Property()
							{
								Id = GetValue(p.Property.Id),
								Name = GetValue(p.Property.Name)
							}
						})
						.ToArrayAsync()
						.ConfigureAwait(false);

					if (items != null && items.Count > DEFAULT_ID)
					{
						IList<ProductFilterItemValue> itemValues = await _context.ProductFilterItemValue
							.Where(i => i.ProductFilterItem.CategoryId == categoryId)
							.Select(i => new ProductFilterItemValue()
							{
								Id = GetValue(i.Id),
								ProductFilterItemId = GetValue(i.ProductFilterItemId),
								Value = GetValue(i.Value)
							})
							.ToArrayAsync()
							.ConfigureAwait(false);

						if (itemValues.Count > DEFAULT_ID)
						{
							for (int i = 0; i < items.Count; i++)
							{
								items[i].ProductFilterItemValues = itemValues
									.Where(itemValue => itemValue.ProductFilterItemId == items[i].Id)
									.Select(itemValue => itemValue)
									.ToArray();
							}
						}
					}

					return items;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return null;
				}
			}

			_logger.LogWarning("GetProductFilterItems: categoryId <= 0");

			return null;
		}

		#endregion
	}
}
