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

		/// <summary>
		/// Connection string.
		/// </summary>
		protected readonly string _connectionString;

		#endregion

		#region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseRepository" /> class.
		/// </summary>
		/// <param name="services">Service provider.</param>
		public BaseRepository(IServiceProvider services, IOptions<AppConfig> config)
		{
			_context = services.GetService<WebEngineContext>();
			_connectionString = config.Value.ConnectionString;
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

		protected int GetValue(int? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return DEFAULT_ID;
		}

		protected string GetValue(string param)
		{
			if (param != null)
			{
				return param;
			}

			return string.Empty;
		}

		protected float GetValue(float? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return 0f;
		}

		protected DateTimeOffset GetValue(DateTimeOffset? param)
		{
			if (param != null)
			{
				return param.Value;
			}

			return new DateTimeOffset();
		}

		protected bool GetValue(bool? param)
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
