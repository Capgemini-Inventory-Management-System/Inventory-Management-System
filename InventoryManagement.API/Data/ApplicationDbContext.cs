using InventoryManagement.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.MobileNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(u => u.UserRole)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // NOTIFICATION
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.NotificationId);

                entity.Property(n => n.Message)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(n => n.ProductName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(n => n.Quantity)
                      .IsRequired();

                entity.Property(n => n.DateCreated)
                      .IsRequired();

                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // PRODUCT
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Description)
                      .HasMaxLength(1000);

                entity.Property(p => p.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Quantity)
                      .IsRequired();
            });

            // CUSTOMER
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);

                entity.Property(c => c.CustomerName)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(c => c.MobileNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(c => c.Email)
                      .IsUnique();
            });

            // ORDER
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);

                entity.Property(o => o.Status)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(o => o.TotalAmount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(o => o.OrderDate)
                      .IsRequired();

                entity.HasOne(o => o.Customer)
                      .WithMany(c => c.Orders)
                      .HasForeignKey(o => o.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ORDER ITEM
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.OrderItemId);

                entity.Property(oi => oi.Quantity)
                      .IsRequired();

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}