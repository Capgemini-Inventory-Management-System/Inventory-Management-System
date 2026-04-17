<<<<<<< HEAD
﻿using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Data

{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ── DbSets ──────────────────────────────────────────────────────────────
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
=======
﻿using Microsoft.EntityFrameworkCore;
using InventoryManagement.API.Models.Entities;

namespace InventoryManagement.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Ye do add karo
        public DbSet<Product> Products { get; set; }
>>>>>>> origin/team-b

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

<<<<<<< HEAD
            // ── Notification ────────────────────────────────────────────────────
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.NotificationId);

                entity.HasOne<IdentityUser>()   // linked to IdentityUser
                      .WithMany()
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(n => n.Message).IsRequired().HasMaxLength(500);
                entity.Property(n => n.ProductName).IsRequired().HasMaxLength(200);
                entity.Property(n => n.DateCreated).IsRequired();
            });

            // ── Product ─────────────────────────────────────────────────────────
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.Quantity).IsRequired();
            });

            // ── Customer ────────────────────────────────────────────────────────
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
                entity.Property(c => c.CustomerName).IsRequired().HasMaxLength(150);
                entity.Property(c => c.MobileNumber).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(200);
            });

            // ── Order ───────────────────────────────────────────────────────────
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderID);
                entity.Property(o => o.Status).IsRequired().HasMaxLength(50);
                entity.Property(o => o.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(o => o.OrderDate).IsRequired();

                // Order → Customer
                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerID)
                      .OnDelete(DeleteBehavior.Restrict);

                // Many-to-Many via OrderItem
                entity.HasMany(o => o.Products)
                      .WithMany(p => p.Orders)
                      .UsingEntity<OrderItem>(
                          j => j.HasOne(oi => oi.Product)
                                .WithMany(p => p.OrderItems)
                                .HasForeignKey(oi => oi.ProductID),
                          j => j.HasOne(oi => oi.Order)
                                .WithMany(o => o.OrderItems)
                                .HasForeignKey(oi => oi.OrderID),
                          j =>
                          {
                              j.HasKey(oi => oi.OrderItemID);
                              j.Property(oi => oi.Quantity).IsRequired();
                          });
            });

            // ── Seed Roles ──────────────────────────────────────────────────────
            SeedRoles(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "2"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "InventoryManager",
                    NormalizedName = "INVENTORYMANAGER",
                    ConcurrencyStamp = "3"
                }
            );
=======
            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

                // Relationship with OrderItem (doosri team ke liye)
                entity.HasMany(p => p.OrderItems)
                      .WithOne(oi => oi.Product)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict); // Product delete nahi hoga agar order items exist karte hain
            });

            
>>>>>>> origin/team-b
        }
    }
}