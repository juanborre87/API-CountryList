using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CountryList.Models;

public partial class DbcountryListContext : DbContext
{
    public DbcountryListContext(){}
    public DbcountryListContext(DbContextOptions<DbcountryListContext> options) : base(options){}
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<HotelRestaurant> HotelRestaurants { get; set; }
    public virtual DbSet<FilteredData> FilteredData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC076F2E7669");

            entity.ToTable("Country");

            entity.Property(e => e.IsoCode).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Population).HasMaxLength(100);
        });

        modelBuilder.Entity<HotelRestaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotel_Re__3214EC0778BDEFE5");

            entity.ToTable("Hotel_Restaurant");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.HotelRestaurants)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("Relation_To_Country");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
