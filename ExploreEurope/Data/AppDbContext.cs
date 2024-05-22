using System;
using System.Reflection.Metadata;
using ExploreEurope.Model;
using Microsoft.EntityFrameworkCore;

namespace ExploreEurope.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{
		}
		public DbSet<Country> Countries { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Attraction> Attractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Cities)
                .WithOne(e => e.Country)
                .HasForeignKey(e => e.CountryId)
                .IsRequired();
            modelBuilder.Entity<City>()
                .HasMany(e => e.Attractions)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId)
                .IsRequired();
        }
    }
}

