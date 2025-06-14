
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
﻿using CertiWeb.API.Users.Domain.Model.Aggregates;
namespace CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Application database context for the Certi Web Platform API.
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   /// <summary>
   ///     On configuring the database context
   /// </summary>
   /// <remarks>
   ///     This method is used to configure the database context.
   ///     It also adds the created and updated date interceptor to the database context.
   /// </remarks>
   /// <param name="builder">
   ///     The option builder for the database context
   /// </param>
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

   /// <summary>
   ///     On creating the database model
   /// </summary>
   /// <remarks>
   ///     This method is used to create the database model for the application.
   /// </remarks>
   /// <param name="builder">
   ///     The model builder for the database context
   /// </param>
   protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // User Context
        builder.Entity<User>().HasKey(d=>d.Id);
        builder.Entity<User>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(d=>d.name).IsRequired();
        builder.Entity<User>().Property(d=>d.email).IsRequired();
        builder.Entity<User>().Property(d=>d.password).IsRequired();
        builder.Entity<User>().Property(d=>d.plan).IsRequired();
        
        // Audit columns for User Context
        builder.Entity<User>().Property(d => d.CreatedDate).HasColumnName("created_at");
        builder.Entity<User>().Property(d => d.UpdatedDate).HasColumnName("updated_at");
        
        // Reservation Configuration
        builder.Entity<CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.UserId).IsRequired();
            entity.Property(r => r.ReservationName).IsRequired().HasMaxLength(100);
            entity.Property(r => r.ReservationEmail).IsRequired().HasMaxLength(100);
            entity.Property(r => r.ImageUrl).IsRequired().HasMaxLength(500);
            entity.Property(r => r.Brand).IsRequired().HasMaxLength(50);
            entity.Property(r => r.Model).IsRequired().HasMaxLength(50);
            entity.Property(r => r.LicensePlate).IsRequired().HasMaxLength(7);
            entity.Property(r => r.InspectionDateTime).IsRequired();
            entity.Property(r => r.Price).IsRequired().HasMaxLength(20);
            entity.Property(r => r.Status).IsRequired().HasMaxLength(20);
            
            // Audit fields mapping
            entity.Property(r => r.CreatedDate).HasColumnName("created_at");
            entity.Property(r => r.UpdatedDate).HasColumnName("updated_at");
            
            entity.ToTable("reservations");
        });
        
        builder.UseSnakeCaseNamingConvention();
    }
    public DbSet<User> Users { get; set; }
}