// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data.Repositories
{
	#region Usings

	using System;

	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	using WebEngine.Core.Config;
	using Microsoft.Extensions.Logging;

	#endregion

	/// <summary>
	/// <see cref="BaseRepository"/> class.
	/// </summary>
	public abstract class BaseRepository<T> : IDisposable where T : class
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

		/// <summary>
		/// Connection string.
		/// </summary>
		protected readonly string _connectionString;

		/// <summary>
		/// The logger.
		/// </summary>
		protected readonly ILogger<T> _logger;

		#endregion

		#region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseRepository" /> class.
		/// </summary>
		/// <param name="services">Service provider.</param>
		public BaseRepository(IServiceProvider services)
		{
			_context = services.GetService<WebEngineContext>();

			IOptions<AppConfig> config = services.GetService<IOptions<AppConfig>>();

			if (config != null)
			{
				_connectionString = config.Value.ConnectionString;
			}

			_logger = services.GetService<ILogger<T>>();
		}

		/// <summary>
		/// Dispose context.
		/// </summary>
		public void Dispose()
		{
			if (_context != null)
			{
				_context.Dispose();
			}
		}

		#endregion

		#region Protected methods

		protected static int GetValue(int? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return DEFAULT_ID;
		}

		protected static string GetValue(string param)
		{
			if (param != null)
			{
				return param;
			}

			return string.Empty;
		}

		protected static float GetValue(float? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return 0f;
		}

		protected static DateTimeOffset GetValue(DateTimeOffset? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return new DateTimeOffset();
		}

		protected static bool GetValue(bool? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return false;
		}

		#endregion
	}
}
