using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using WeatherAPI.Models;

#nullable disable

namespace WeatherAPI.DbContext
{
    public partial class WeatherDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WeatherDbContext()
        {
        }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("cities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoordLat)
                    .HasColumnType("FLOAT")
                    .HasColumnName("coord_lat");

                entity.Property(e => e.CoordLon)
                    .HasColumnType("FLOAT")
                    .HasColumnName("coord_lon");

                entity.Property(e => e.Country)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("name");

                entity.Property(e => e.State)
                    .HasColumnType("VARCHAR")
                    .HasColumnName("state");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
