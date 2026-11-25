using Floris.Models;
using Microsoft.EntityFrameworkCore;

namespace Floris.Data
{
    /// <summary>Контекст базы данных приложения Floris (Entity Framework Core).</summary>
    public class FlorisDbContext : DbContext
    {
        /// <summary>Создаёт контекст с заданными параметрами подключения.</summary>
        /// <param name="options">Параметры контекста (строка подключения, провайдер БД).</param>
        public FlorisDbContext(DbContextOptions<FlorisDbContext> options) : base(options) { }

        /// <summary>Пользователи.</summary>
        public DbSet<User> Users { get; set; } = null!;
        /// <summary>Роли.</summary>
        public DbSet<Role> Roles { get; set; } = null!;
        /// <summary>Города.</summary>
        public DbSet<City> Cities { get; set; } = null!;
        /// <summary>Магазины.</summary>
        public DbSet<Store> Stores { get; set; } = null!;
        /// <summary>Категории каталога.</summary>
        public DbSet<Category> Categories { get; set; } = null!;
        /// <summary>Товары.</summary>
        public DbSet<Product> Products { get; set; } = null!;
        /// <summary>Партии товаров на складах.</summary>
        public DbSet<Inventory> Inventory { get; set; } = null!;
        /// <summary>Заказы.</summary>
        public DbSet<Order> Orders { get; set; } = null!;
        /// <summary>Позиции заказов.</summary>
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        /// <summary>Собранные букеты.</summary>
        public DbSet<CustomBouquet> CustomBouquets { get; set; } = null!;
        /// <summary>Компоненты собранных букетов.</summary>
        public DbSet<BouquetComponent> BouquetComponents { get; set; } = null!;
        /// <summary>Слоты доставки.</summary>
        public DbSet<DeliverySlot> DeliverySlots { get; set; } = null!;
        /// <summary>Адреса пользователей.</summary>
        public DbSet<UserAddress> UserAddresses { get; set; } = null!;
        /// <summary>Платежи.</summary>
        public DbSet<Payment> Payments { get; set; } = null!;
        /// <summary>Фото-отчёты.</summary>
        public DbSet<PhotoReport> PhotoReports { get; set; } = null!;

        /// <summary>Общие правила хранения типов: деньги — decimal(18,2), перечисления — строки.</summary>
        /// <param name="configurationBuilder">Построитель соглашений модели.</param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);

            configurationBuilder.Properties<OrderStatus>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<DeliveryMethod>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<PaymentMethod>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<PaymentStatus>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<ProductType>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<StockStatus>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<PhotoType>().HaveConversion<string>().HaveMaxLength(30);
            configurationBuilder.Properties<BouquetComponentRole>().HaveConversion<string>().HaveMaxLength(30);
        }

        /// <summary>Настройка связей, индексов и ограничений. Каскадное удаление разрешено только для «дочерних» таблиц.</summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----- Пользователи и роли (одна роль на пользователя) -----
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role).WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();

            modelBuilder.Entity<UserAddress>()
                .HasOne(a => a.User).WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserAddress>().Property(a => a.Latitude).HasPrecision(9, 6);
            modelBuilder.Entity<UserAddress>().Property(a => a.Longitude).HasPrecision(9, 6);

            // ----- Города и магазины -----
            modelBuilder.Entity<Store>()
                .HasOne(s => s.City).WithMany(c => c.Stores)
                .HasForeignKey(s => s.CityId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Store>().Property(s => s.Latitude).HasPrecision(9, 6);
            modelBuilder.Entity<Store>().Property(s => s.Longitude).HasPrecision(9, 6);

            // ----- Каталог -----
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Parent).WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().HasIndex(c => c.Slug).IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category).WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);

            // ----- Склад: строка Inventory — партия, поэтому индекс не уникальный -----
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product).WithMany(p => p.Inventory)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Store).WithMany(s => s.Inventory)
                .HasForeignKey(i => i.StoreId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Inventory>().HasIndex(i => new { i.StoreId, i.ProductId });

            // ----- Слоты доставки -----
            modelBuilder.Entity<DeliverySlot>()
                .HasOne(ds => ds.Store).WithMany(s => s.DeliverySlots)
                .HasForeignKey(ds => ds.StoreId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DeliverySlot>().HasIndex(ds => new { ds.StoreId, ds.SlotDate });

            // ----- Заказы -----
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User).WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store).WithMany()
                .HasForeignKey(o => o.StoreId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryAddress).WithMany()
                .HasForeignKey(o => o.DeliveryAddressId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliverySlot).WithMany()
                .HasForeignKey(o => o.DeliverySlotId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasIndex(o => new { o.StoreId, o.Status });

            // ----- Позиции заказа: ровно одно из ProductId / CustomBouquetId -----
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order).WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product).WithMany()
                .HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.CustomBouquet).WithMany()
                .HasForeignKey(oi => oi.CustomBouquetId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>().ToTable(t => t.HasCheckConstraint(
                "CK_OrderItems_ProductOrBouquet",
                "([ProductId] IS NOT NULL AND [CustomBouquetId] IS NULL) OR ([ProductId] IS NULL AND [CustomBouquetId] IS NOT NULL)"));

            // ----- Конструктор букетов -----
            modelBuilder.Entity<BouquetComponent>()
                .HasOne(bc => bc.CustomBouquet).WithMany(cb => cb.Components)
                .HasForeignKey(bc => bc.CustomBouquetId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BouquetComponent>()
                .HasOne(bc => bc.Product).WithMany()
                .HasForeignKey(bc => bc.ProductId).OnDelete(DeleteBehavior.NoAction);

            // ----- Платежи и фото-отчёты -----
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order).WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhotoReport>()
                .HasOne(pr => pr.Order).WithMany(o => o.Photos)
                .HasForeignKey(pr => pr.OrderId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PhotoReport>()
                .HasOne(pr => pr.UploadedByUser).WithMany()
                .HasForeignKey(pr => pr.UploadedByUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
