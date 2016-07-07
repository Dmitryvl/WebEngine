// -----------------------------------------------------------------------
// <copyright file="Startup.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Web
{
	#region Usings

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json.Serialization;
	using WebEngine.Core.Config;
	using WebEngine.Core.Interfaces;
	using WebEngine.Data;
	using WebEngine.Data.Repositories;

	#endregion

	public class Startup
	{
		#region Constructors

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		#endregion

		#region Properties

		public IConfigurationRoot Configuration { get; }

		#endregion

		#region Public methods

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<WebEngineContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			services.AddOptions();

			services.Configure<AppConfig>(opt =>
			{
				opt.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
			});

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IStoreRepository, StoreRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient<ICategotyRepository, CategoryRepository>();
			services.AddTransient<IProductFilterRepository, ProductFilterRepository>();
			services.AddTransient<IProductPageRepository, ProductPageRepository>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationScheme = "Cookies",
				LoginPath = new PathString("/Account/Login"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				CookieName = "websettings"
			});

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

			//InitData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
		}

		#endregion
	}
}
