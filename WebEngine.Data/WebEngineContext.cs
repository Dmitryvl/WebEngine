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

		/// <summary>
		/// Gets or sets companies.
		/// </summary>
		public DbSet<Company> Companies { get; set; }

		/// <summary>
		/// Gets or sets categories.
		/// </summary>
		public DbSet<Category> Categories { get; set; }

		/// <summary>
		/// Gets or sets data types.
		/// </summary>
		public DbSet<DataType> DataTypes { get; set; }

		/// <summary>
		/// Gets or sets smartphone base properties.
		/// </summary>
		public DbSet<ProductBaseProperty> ProductBaseProperties { get; set; }

		/// <summary>
		/// Gets or sets smartphone properties.
		/// </summary>
		public DbSet<ProductProperty> ProductProperties { get; set; }

		/// <summary>
		/// Gets or sets smartphones.
		/// </summary>
		public DbSet<Product> Products { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ProductToProperty"/>.
		/// </summary>
		public DbSet<ProductToProperty> ProductToProperty { get; set; }

		/// <summary>
		/// Gets or sets smartphone offers.
		/// </summary>
		public DbSet<ProductOffer> ProductOffers { get; set; }

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

			const int stringLength = 240;

			builder.Entity<DataType>().HasKey(d => d.Id);
			builder.Entity<DataType>().Property(d => d.Name).IsRequired().HasMaxLength(20);

			builder.Entity<User>().HasKey(u => u.Id);
			builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(stringLength);
			builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(stringLength);
			builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(64);
			builder.Entity<User>().Property(u => u.PasswordSalt).IsRequired().HasMaxLength(10);

			builder.Entity<Role>().HasKey(r => r.Id);
			builder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<Store>().HasKey(s => s.Id);
			builder.Entity<Store>().Property(s => s.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<Country>().HasKey(c => c.Id);
			builder.Entity<Country>().Property(c => c.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<Region>().HasKey(c => c.Id);
			builder.Entity<Region>().Property(c => c.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<City>().HasKey(c => c.Id);
			builder.Entity<City>().Property(c => c.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<Company>().HasKey(c => c.Id);
			builder.Entity<Company>().Property(c => c.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<Category>().HasKey(c => c.Id);
			builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<ProductBaseProperty>().HasKey(s => s.Id);
			builder.Entity<ProductBaseProperty>().Property(s => s.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<ProductProperty>().HasKey(s => s.Id);
			builder.Entity<ProductProperty>().Property(s => s.Name).IsRequired().HasMaxLength(stringLength);

			builder.Entity<ProductToProperty>().Property(s => s.Value).HasMaxLength(stringLength);
			builder.Entity<ProductToProperty>().Property(s => s.SizeValue).HasMaxLength(stringLength);

			builder.Entity<Product>().HasKey(s => s.Id);
			builder.Entity<Product>().Property(s => s.Name).IsRequired().HasMaxLength(stringLength);

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

			builder.Entity<ProductProperty>()
				.HasOne(s => s.ProductBaseProperty)
				.WithMany(c => c.ProductProperties)
				.HasForeignKey(s => s.ProductBasePropertyId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ProductProperty>()
				.HasOne(s => s.DataType)
				.WithMany(c => c.ProductProperties)
				.HasForeignKey(s => s.DataTypeId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Product>()
				.HasOne(s => s.Company)
				.WithMany(c => c.Products)
				.HasForeignKey(s => s.CompanyId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Product>()
				.HasOne(s => s.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(s => s.CategoryId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ProductOffer>()
				.HasKey(s => new { s.StoreId, s.ProductId });

			builder.Entity<ProductOffer>()
				.HasOne(s => s.Store)
				.WithMany(st => st.ProductOffers)
				.HasForeignKey(s => s.StoreId);

			builder.Entity<ProductOffer>()
				.HasOne(s => s.Product)
				.WithMany(sm => sm.ProductOffer)
				.HasForeignKey(s => s.ProductId);

			builder.Entity<ProductToProperty>()
				.HasKey(s => new { s.ProductId, s.ProductPropertyId });

			builder.Entity<ProductToProperty>()
				.HasOne(s => s.Product)
				.WithMany(st => st.ProductToProperty)
				.HasForeignKey(s => s.ProductId);

			builder.Entity<ProductToProperty>()
				.HasOne(s => s.ProductProperty)
				.WithMany(sm => sm.ProductToProperty)
				.HasForeignKey(s => s.ProductPropertyId);

			#endregion
		}

		#endregion
	}
}
