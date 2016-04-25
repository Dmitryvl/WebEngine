// -----------------------------------------------------------------------
// <copyright file="Startup.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web
{
	#region Usings

	using Microsoft.AspNet.Builder;
	using Microsoft.AspNet.Hosting;
	using Microsoft.Data.Entity;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	using WebEngine.Data;
	using WebEngine.Core.Interfaces;
	using WebEngine.Data.Repositories;

	#endregion

	/// <summary>
	/// <see cref="Startup"/> class.
	/// </summary>
	public class Startup
	{
		#region Constructors

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets();

				builder.AddApplicationInsightsSettings(developerMode: true);
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		#endregion

		#region Properties

		public IConfigurationRoot Configuration { get; set; }

		#endregion

		#region Public methods

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplicationInsightsTelemetry(Configuration);

			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<WebEngineContext>(options =>
					options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

			services.AddMvc();

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IStoreRepository, StoreRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();

		}


		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseApplicationInsightsRequestTelemetry();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}

			app.UseCookieAuthentication(options =>
			{
				options.AuthenticationScheme = "Cookies";
				options.LoginPath = new Microsoft.AspNet.Http.PathString("/Account/Login");
				options.AutomaticAuthenticate = true;
				options.AutomaticChallenge = true;
				options.CookieName = "websettings";
			});

			app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areaRoute",
					template: "{area:exists}/{controller}/{action}",
					defaults: new { action = "Index" });

				routes.MapRoute(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "Home", action = "Index" });

				routes.MapRoute(
					name: "api",
					template: "{controller}/{id?}");
			});

			InitData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
		}

		public static void Main(string[] args) => WebApplication.Run<Startup>(args);

		#endregion
	}
}
