// -----------------------------------------------------------------------
// <copyright file="WebEngineContext.cs" author="Dzmitry Prakapenka">
//     All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WebEngine.Data
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Data.Entity;
	using Core.Entities;
	#endregion

	/// <summary>
	/// <see cref="WebEngineContext"/> class.
	/// </summary>
	public class WebEngineContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>().HasKey(u => u.Id);
			builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(240);

			builder.Entity<Role>().HasKey(r => r.Id);
			builder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(120);

			// Refs
			builder.Entity<User>().HasOne(u => u.Role)
				.WithMany(r => r.Users).HasForeignKey(u => u.RoleId).IsRequired();
		}
	}
}
