using ECommMVC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.DAL.Context
{
    public class ECommContext : DbContext
    {
        public ECommContext() { }

        public ECommContext(DbContextOptions<ECommContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-K3QEGEG\\SQLEXPRESS;Database=ECommMVC_DB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category
            modelBuilder.Entity<Category>().HasKey(x => x.ID);
            modelBuilder.Entity<Category>().Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.Description)
                .HasMaxLength(150);

            // Coupon
            modelBuilder.Entity<Coupon>().HasKey(x => x.ID);
            modelBuilder.Entity<Coupon>().Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Coupon>().Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Coupon>().Property(x => x.Discount)
                .HasColumnType("decimal(6, 3)")
                .HasPrecision(6, 3)
                .IsRequired();
            modelBuilder.Entity<Coupon>().Property(x => x.ActivatedAt)
                .IsRequired();
            modelBuilder.Entity<Coupon>().Property(x => x.PassivedAt)
                .IsRequired();

            // Order
            modelBuilder.Entity<Order>().HasKey(x => x.ID);
            modelBuilder.Entity<Order>().Property(x => x.Freight)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.OrderDate)
                .IsRequired();
            modelBuilder.Entity<Order>().Property(x => x.ShipAddress)
                .IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<Order>().Property(x => x.ShipCity)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Order>().Property(x => x.ShipRegion)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Order>().Property(x => x.ShipPostalCode)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Order>().Property(x => x.ShipCountry)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shipper)
                .WithMany(m => m.Orders)
                .HasForeignKey(x => x.ShipperID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(m => m.Orders)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(w => w.Order)
                .HasForeignKey<Payment>(x => x.OrderID)
                .OnDelete(DeleteBehavior.SetNull);

            // Order Details
            modelBuilder.Entity<OrderDetail>().HasKey(x => x.ID);
            modelBuilder.Entity<OrderDetail>().Property(x => x.UnitPrice)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(x => x.Quantity)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(x => x.Discount)
                .HasColumnType("decimal(6, 3)")
                .HasPrecision(6, 3)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(x => x.VAT)
                .HasColumnType("decimal(6, 3)")
                .HasPrecision(6, 3)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(x => x.TotalPrice)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(x => x.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Product)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(x => x.ProductID)
                .OnDelete(DeleteBehavior.SetNull);

            // Payment
            modelBuilder.Entity<Payment>().HasKey(x => x.ID);
            modelBuilder.Entity<Payment>().Property(x => x.PaymentMethod)
                .IsRequired();
            modelBuilder.Entity<Payment>().Property(x => x.Freight)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<Payment>()
                .HasOne(o => o.Order)
                .WithOne(m => m.Payment)
                .HasForeignKey<Order>(x => x.PaymentID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
                .HasOne(o => o.User)
                .WithMany(m => m.Payments)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.SetNull);

            // Product
            modelBuilder.Entity<Product>().HasKey(x => x.ID);
            modelBuilder.Entity<Product>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<Product>().Property(x => x.Description)
                .HasMaxLength(5000);
            modelBuilder.Entity<Product>().Property(x => x.UnitPrice)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Quantity)
                .IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.VAT)
                .HasColumnType("decimal(6, 3)")
                .HasPrecision(6, 3)
                .IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Discount)
                .HasColumnType("decimal(6, 3)")
                .HasPrecision(6, 3)
                .IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.TotalPrice)
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<Product>()
                .HasOne(o => o.Category)
                .WithMany(m => m.Products)
                .HasForeignKey(x => x.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            // Review
            modelBuilder.Entity<Review>().HasKey(x => x.ID);
            modelBuilder.Entity<Review>().Property(x => x.Rating)
                .IsRequired();
            modelBuilder.Entity<Review>()
                .HasOne(o => o.Product)
                .WithMany(m => m.Reviews)
                .HasForeignKey(x => x.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>()
                .HasOne(o => o.User)
                .WithMany(m => m.Reviews)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Shipper
            modelBuilder.Entity<Shipper>().HasKey(x => x.ID);
            modelBuilder.Entity<Shipper>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            // Ticket
            modelBuilder.Entity<Ticket>().HasKey(x => x.ID);
            modelBuilder.Entity<Ticket>().Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<Ticket>().Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(5000);
            modelBuilder.Entity<Ticket>()
                .HasOne(o => o.User)
                .WithMany(m => m.Tickets)
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User
            modelBuilder.Entity<User>().HasKey(x => x.ID);
            modelBuilder.Entity<User>().Property(x => x.Identity)
                .IsRequired()
                .HasMaxLength(11);
            modelBuilder.Entity<User>().Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>().Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(250);
            modelBuilder.Entity<User>().Property(x => x.City)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<User>().Property(x => x.Region)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.PostalCode)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<User>().Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<User>().Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
