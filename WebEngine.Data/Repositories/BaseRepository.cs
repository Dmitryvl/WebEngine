// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// <see cref="BaseRepository"/> class.
	/// </summary>
	public abstract class BaseRepository
	{
		#region Protected fields

		/// <summary>
		/// Default id.
		/// </summary>
		protected const int DEFAULT_ID = 0;

		/// <summary>
		/// Database context.
		/// </summary>
		protected readonly WebEngineContext _context;

		#endregion

		#region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseRepository" /> class.
		/// </summary>
		/// <param name="context">Database context.</param>
		/// <param name="loggerFactory">Logger factory</param>
		public BaseRepository(IServiceProvider services)
		{
			_context = services.GetService<WebEngineContext>();
		}

		/// <summary>
		/// Dispose context.
		/// </summary>
		public void Dispose()
		{
			_context?.Dispose();
		}

		#endregion
	}
}
