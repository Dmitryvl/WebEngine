// -----------------------------------------------------------------------
// <copyright file="Program.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web
{
	#region Usings

	using System.IO;

	using Microsoft.AspNetCore.Hosting;

	#endregion

	/// <summary>
	/// <see cref="Program"/> class.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Start web site.
		/// </summary>
		/// <param name="args">Input parameters.</param>
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
