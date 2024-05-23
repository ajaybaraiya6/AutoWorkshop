using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoWorkshop.Models
{
    public partial class workshopDBContext : DbContext
    {
        public workshopDBContext()
        {
        }

        public workshopDBContext(DbContextOptions<workshopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleService> VehicleServices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("customer_address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("last_name");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("slots");

                entity.Property(e => e.SlotId)
                    .ValueGeneratedNever()
                    .HasColumnName("slot_id");

                entity.Property(e => e.AssignedHours).HasColumnName("assigned_hours");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleNumber)
                    .HasName("PK__vehicle__2D703C2B1C8A0C3F");

                entity.ToTable("vehicle");

                entity.Property(e => e.VehicleNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("vehicle_number");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.VehicleMake)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vehicle_make");

                entity.Property(e => e.VehicleModel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vehicle_model");

                entity.Property(e => e.VehicleYear).HasColumnName("vehicle_year");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("fk_vehicle_customer_id");
            });

            modelBuilder.Entity<VehicleService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__vehicle___3E0DB8AFFBDBA295");

                entity.ToTable("vehicle_services");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.ServiceDate)
                    .HasColumnType("datetime")
                    .HasColumnName("service_date");

                entity.Property(e => e.SlotId).HasColumnName("slot_id");

                entity.Property(e => e.VehicleNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("vehicle_number");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.VehicleServices)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("fk_vehicle_service_slot_id");

                entity.HasOne(d => d.VehicleNumberNavigation)
                    .WithMany(p => p.VehicleServices)
                    .HasForeignKey(d => d.VehicleNumber)
                    .HasConstraintName("fk_vehicle_service_vehicle_number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
