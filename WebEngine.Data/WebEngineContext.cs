// -----------------------------------------------------------------------
// <copyright file="WebEngineContext.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data
{
	#region Usings

	using Microsoft.Data.Entity;
	using Microsoft.Data.Entity.Metadata;

	using WebEngine.Core.Entities;

	#endregion

	/// <summary>
	/// <see cref="WebEngineContext"/> class.
	/// </summary>
	public class WebEngineContext : DbContext
	{
		#region Public properties

		/// <summary>
		/// Gets or sets users.
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Gets or sets roles.
		/// </summary>
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Gets or sets stores.
		/// </summary>
		public DbSet<Store> Stores { get; set; }

		/// <summary>
		/// Gets or sets countries.
		/// </summary>
		public DbSet<Country> Countries { get; set; }

		/// <summary>
		/// Gets or sets regions.
		/// </summary>
		public DbSet<Region> Regions { get; set; }

		/// <summary>
		/// Gets or sets cities.
		/// </summary>
		public DbSet<City> Cities { get; set; }

		#endregion

		#region Override

		/// <summary>
		/// Create entities map.
		/// </summary>
		/// <param name="builder">Model builder.</param>
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			#region Properties

			builder.Entity<User>().HasKey(u => u.Id);
			builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(240);
			builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(240);
			builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(64);
			builder.Entity<User>().Property(u => u.PasswordSalt).IsRequired().HasMaxLength(10);

			builder.Entity<Role>().HasKey(r => r.Id);
			builder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(120);

			builder.Entity<Store>().HasKey(s => s.Id);
			builder.Entity<Store>().Property(s => s.Name).IsRequired().HasMaxLength(240);

			builder.Entity<Country>().HasKey(c => c.Id);
			builder.Entity<Country>().Property(c => c.Name).IsRequired().HasMaxLength(240);

			builder.Entity<Region>().HasKey(c => c.Id);
			builder.Entity<Region>().Property(c => c.Name).IsRequired().HasMaxLength(240);

			builder.Entity<City>().HasKey(c => c.Id);
			builder.Entity<City>().Property(c => c.Name).IsRequired().HasMaxLength(240);

			#endregion

			#region Relations

			builder.Entity<User>()
				.HasOne(u => u.Role)
				.WithMany(r => r.Users)
				.HasForeignKey(u => u.RoleId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Store>()
				.HasOne(s => s.User)
				.WithMany(u => u.Stores)
				.HasForeignKey(s => s.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Region>()
				.HasOne(r => r.Country)
				.WithMany(c => c.Regions)
				.HasForeignKey(r => r.CountryId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<City>()
				.HasOne(c => c.Region)
				.WithMany(r => r.Cities)
				.HasForeignKey(c => c.RegionId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			#endregion
		}

		#endregion
	}
}
