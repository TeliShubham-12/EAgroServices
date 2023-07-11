
using Shipments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Shipments.Repositories.Contexts
{
    public class ShipmentContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _conString;

        public ShipmentContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _conString =
                this._configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DbSet<Shipment> Shipments { get;set;}
        public DbSet<ShipmentItem> ShipmentItems { get;set;}
        public DbSet<Vehicle> Vehicles { get;set;}
        public DbSet<GoodsCollection> GoodsCollections { get;set;}
        public DbSet<VerifiedCollection> VerifiedCollections { get;set;}
        public DbSet<Crop> Crops { get;set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(
                _conString ?? throw new InvalidOperationException("Connection string is null.")
            );
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.VehicleId);
                entity.Property(e => e.MerchantId);
                entity.Property(e => e.Kilometers);
                entity.Property(e => e.Status);
                entity.Property(e => e.ShipmentDate);
                modelBuilder.Entity<Shipment>().ToTable("shipments");
            });

             modelBuilder.Entity<ShipmentItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ShipmentId);
                entity.Property(e => e.CollectionId);
                modelBuilder.Entity<ShipmentItem>().ToTable("shipmentitems");
            });

             modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TransporterId);
                entity.Property(e => e.VehicleType);
                entity.Property(e => e.RtoNumber);
                modelBuilder.Entity<Vehicle>().ToTable("vehicles");
            });
             modelBuilder.Entity<GoodsCollection>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CollectionCenterId);
                entity.Property(e => e.FarmerId);
                entity.Property(e => e.CropId);
                entity.Property(e => e.ContainerType);
                entity.Property(e => e.Quantity);
                entity.Property(e => e.Weight);
                entity.Property(e => e.CollectionDate);
                modelBuilder.Entity<GoodsCollection>().ToTable("goodscollections");
            });

             modelBuilder.Entity<VerifiedCollection>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CollectionId);
                entity.Property(e => e.Grade);
                entity.Property(e => e.Weight);
                entity.Property(e => e.InspectorId);
                entity.Property(e => e.InspectionDate);
                modelBuilder.Entity<VerifiedCollection>().ToTable("verifiedgoodscollection");
            });

             modelBuilder.Entity<Crop>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title);
                entity.Property(e => e.ImageUrl);
                entity.Property(e => e.Rate);
                modelBuilder.Entity<Crop>().ToTable("crops");
            });
        }
    }
}