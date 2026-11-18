using Floris.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Floris.Data
{
    public class FlorisDbContext : DbContext
    {
        public FlorisDbContext(DbContextOptions<FlorisDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CustomBouquet> CustomBouquets { get; set; }
        public DbSet<BouquetComponent> BouquetComponents { get; set; }
        public DbSet<DeliverySlot> DeliverySlots { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PhotoReport> PhotoReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // User <-> Role через UserRole
            // =========================

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Категории: дерево категорий
            // =========================

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Магазины и города
            // =========================

            modelBuilder.Entity<Store>()
                .HasOne(s => s.City)
                .WithMany(c => c.Stores)
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Товары и категории
            // =========================

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Остатки товаров в магазинах
            // =========================

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Inventory)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Store)
                .WithMany(s => s.Inventory)
                .HasForeignKey(i => i.StoreId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Адреса пользователя
            // =========================

            modelBuilder.Entity<UserAddress>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Временные слоты доставки
            // =========================

            modelBuilder.Entity<DeliverySlot>()
                .HasOne(ds => ds.Store)
                .WithMany(s => s.DeliverySlots)
                .HasForeignKey(ds => ds.StoreId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Заказы
            // Самое важное исправление
            // =========================

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany()
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryAddress)
                .WithMany()
                .HasForeignKey(o => o.DeliveryAddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliverySlot)
                .WithMany()
                .HasForeignKey(o => o.DeliverySlotId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Позиции заказа
            // =========================

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.CustomBouquet)
                .WithMany()
                .HasForeignKey(oi => oi.CustomBouquetId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Кастомные букеты
            // =========================

            modelBuilder.Entity<BouquetComponent>()
                .HasOne(bc => bc.CustomBouquet)
                .WithMany(cb => cb.Components)
                .HasForeignKey(bc => bc.CustomBouquetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BouquetComponent>()
                .HasOne(bc => bc.Product)
                .WithMany()
                .HasForeignKey(bc => bc.ProductId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Оплата
            // =========================

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Фотоотчёты
            // =========================

            modelBuilder.Entity<PhotoReport>()
                .HasOne(pr => pr.Order)
                .WithMany(o => o.Photos)
                .HasForeignKey(pr => pr.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhotoReport>()
                .HasOne(pr => pr.UploadedByUser)
                .WithMany()
                .HasForeignKey(pr => pr.UploadedByUserId)
                .OnDelete(DeleteBehavior.NoAction);


            // =========================
            // Начальные данные
            // =========================

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "client" },
                new Role { Id = 2, Name = "florist" },
                new Role { Id = 3, Name = "courier" },
                new Role { Id = 4, Name = "admin" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Москва", Timezone = "Europe/Moscow" }
            );
        }
    }
}