using Floris.Models;

namespace Floris.Data
{
    /// <summary>
    /// Заполняет пустую базу начальными данными: роли, города, магазины, категории, товары, остатки.
    /// Повторный запуск безопасен: каждый шаг пропускается, если данные уже есть.
    /// </summary>
    public static class DbSeeder
    {
        /// <summary>Выполняет все шаги заполнения базы.</summary>
        /// <param name="db">Контекст базы данных.</param>
        public static void Seed(FlorisDbContext db)
        {
            SeedRoles(db);
            SeedCitiesAndStores(db);
            SeedCategories(db);
            SeedProductsAndInventory(db);
        }

        /// <summary>Добавляет четыре роли системы.</summary>
        /// <param name="db">Контекст базы данных.</param>
        private static void SeedRoles(FlorisDbContext db)
        {
            if (db.Roles.Any()) return;

            db.Roles.AddRange(
                new Role { Name = RoleNames.Client },
                new Role { Name = RoleNames.Florist },
                new Role { Name = RoleNames.Courier },
                new Role { Name = RoleNames.Admin });
            db.SaveChanges();
        }

        /// <summary>Добавляет города и магазины (адреса и координаты условные).</summary>
        /// <param name="db">Контекст базы данных.</param>
        private static void SeedCitiesAndStores(FlorisDbContext db)
        {
            if (db.Cities.Any()) return;

            var orenburg = new City { Name = "Оренбург", Timezone = "Asia/Yekaterinburg" };
            orenburg.Stores.Add(new Store
            {
                Name = "Floris на Советской",
                Address = "г. Оренбург, ул. Советская, 20",
                Latitude = 51.768000m,
                Longitude = 55.100000m,
                Phone = "+7 (3532) 00-00-01"
            });
            orenburg.Stores.Add(new Store
            {
                Name = "Floris на Правде",
                Address = "г. Оренбург, ул. Правды, 10",
                Latitude = 51.790000m,
                Longitude = 55.130000m,
                Phone = "+7 (3532) 00-00-02"
            });

            var moscow = new City { Name = "Москва", Timezone = "Europe/Moscow" };
            moscow.Stores.Add(new Store
            {
                Name = "Floris на Тверской",
                Address = "г. Москва, ул. Тверская, 12",
                Latitude = 55.760000m,
                Longitude = 37.609000m,
                Phone = "+7 (495) 000-00-01"
            });

            var spb = new City { Name = "Санкт-Петербург", Timezone = "Europe/Moscow" };
            spb.Stores.Add(new Store
            {
                Name = "Floris на Невском",
                Address = "г. Санкт-Петербург, Невский пр., 28",
                Latitude = 59.935800m,
                Longitude = 30.325600m,
                Phone = "+7 (812) 000-00-01"
            });

            db.Cities.AddRange(orenburg, moscow, spb);
            db.SaveChanges();
        }

        /// <summary>Добавляет дерево категорий каталога.</summary>
        /// <param name="db">Контекст базы данных.</param>
        private static void SeedCategories(FlorisDbContext db)
        {
            if (db.Categories.Any()) return;

            var bouquets = new Category { Name = "Букеты", Slug = "buketi" };
            bouquets.Children.Add(new Category { Name = "Монобукеты", Slug = "monobukety" });
            bouquets.Children.Add(new Category { Name = "Микс-букеты", Slug = "miks-bukety" });

            var compositions = new Category { Name = "Композиции", Slug = "kompozicii" };
            compositions.Children.Add(new Category { Name = "Корзины", Slug = "korziny" });

            var flowers = new Category { Name = "Цветы поштучно", Slug = "cvety" };
            flowers.Children.Add(new Category { Name = "Розы", Slug = "rozy" });
            flowers.Children.Add(new Category { Name = "Тюльпаны", Slug = "tyulpany" });
            flowers.Children.Add(new Category { Name = "Зелень", Slug = "zelen" });

            var accessories = new Category { Name = "Аксессуары", Slug = "aksessuary" };
            accessories.Children.Add(new Category { Name = "Открытки", Slug = "otkrytki" });
            accessories.Children.Add(new Category { Name = "Вазы", Slug = "vazy" });
            accessories.Children.Add(new Category { Name = "Упаковка", Slug = "upakovka" });

            db.Categories.AddRange(bouquets, compositions, flowers, accessories);
            db.SaveChanges();
        }

        /// <summary>Добавляет товары и раскладывает их по складам всех магазинов (с разными статусами для демонстрации).</summary>
        /// <param name="db">Контекст базы данных.</param>
        private static void SeedProductsAndInventory(FlorisDbContext db)
        {
            if (db.Products.Any()) return;

            var cat = db.Categories.ToDictionary(c => c.Slug);

            var products = new List<Product>
            {
                // Готовые букеты и композиции
                NewProduct("Букет «Нежность»", "25 розовых роз с эвкалиптом", ProductType.Bouquet, cat["miks-bukety"], 3500m, 4),
                NewProduct("Букет «Страсть»", "51 красная роза", ProductType.Bouquet, cat["monobukety"], 7200m, 4),
                NewProduct("Букет «Весна»", "Тюльпаны микс, 35 шт.", ProductType.Bouquet, cat["monobukety"], 4100m, 3),
                NewProduct("Букет «Солнечный день»", "Подсолнухи и хризантемы с зеленью", ProductType.Bouquet, cat["miks-bukety"], 2900m, 4),
                NewProduct("Букет «Белое облако»", "Белые розы, гипсофила и эвкалипт", ProductType.Bouquet, cat["miks-bukety"], 3900m, 4),
                NewProduct("Букет «Пионовый рай»", "15 пионов, упаковка крафт", ProductType.Bouquet, cat["monobukety"], 5400m, 3),
                NewProduct("Корзина «Летний сад»", "Цветочная композиция в плетёной корзине", ProductType.Bouquet, cat["korziny"], 4800m, 5),

                // Цветы поштучно
                NewProduct("Роза красная 50 см", "Эквадорская роза, цена за стебель", ProductType.Flower, cat["rozy"], 140m, 7),
                NewProduct("Роза розовая 50 см", "Эквадорская роза, цена за стебель", ProductType.Flower, cat["rozy"], 140m, 7),
                NewProduct("Тюльпан микс", "Тюльпаны разных цветов, цена за стебель", ProductType.Flower, cat["tyulpany"], 90m, 5),
                NewProduct("Эвкалипт", "Веточка эвкалипта", ProductType.Flower, cat["zelen"], 60m, 10),
                NewProduct("Питтоспорум", "Веточка питтоспорума", ProductType.Flower, cat["zelen"], 55m, 10),

                // Сопутствующие товары
                NewProduct("Открытка", "Открытка с вашим текстом", ProductType.Accessory, cat["otkrytki"], 120m, 365),
                NewProduct("Ваза стеклянная", "Прозрачная ваза, 25 см", ProductType.Accessory, cat["vazy"], 650m, 365),
                NewProduct("Крафт-упаковка", "Упаковка из крафт-бумаги", ProductType.Accessory, cat["upakovka"], 100m, 365),
                NewProduct("Атласная лента", "Лента для оформления букета", ProductType.Accessory, cat["upakovka"], 80m, 365)
            };
            db.Products.AddRange(products);

            var now = DateTime.UtcNow;
            var counter = 0;
            foreach (var store in db.Stores.ToList())
            {
                foreach (var product in products)
                {
                    counter++;

                    // Детерминированно создаём разные ситуации: нет в наличии, мало, уценка, достаточно.
                    var quantity = counter % 7 == 0 ? 0 : counter % 5 == 0 ? 3 : 10 + counter % 15;
                    var status = quantity == 0 ? StockStatus.OutOfStock
                               : quantity <= 5 ? StockStatus.Low
                               : StockStatus.Available;
                    var price = product.BasePrice;

                    if (counter % 11 == 0 && quantity > 0)
                    {
                        status = StockStatus.Markdown;
                        price = Math.Round(product.BasePrice * 0.7m, 0);
                    }

                    db.Inventory.Add(new Inventory
                    {
                        Product = product,
                        Store = store,
                        Quantity = quantity,
                        CurrentPrice = price,
                        ReceivedAt = now.AddDays(-1),
                        ExpiresAt = now.AddDays(product.ShelfLifeDays),
                        Status = status
                    });
                }
            }

            db.SaveChanges();
        }

        /// <summary>Создаёт объект товара.</summary>
        /// <param name="name">Название.</param>
        /// <param name="description">Описание.</param>
        /// <param name="type">Тип товара.</param>
        /// <param name="category">Категория.</param>
        /// <param name="price">Базовая цена.</param>
        /// <param name="shelfLifeDays">Срок хранения в днях.</param>
        /// <returns>Новый товар (ещё не сохранён в БД).</returns>
        private static Product NewProduct(string name, string description, ProductType type,
                                          Category category, decimal price, int shelfLifeDays)
        {
            return new Product
            {
                Name = name,
                Description = description,
                Type = type,
                Category = category,
                BasePrice = price,
                ShelfLifeDays = shelfLifeDays
            };
        }
    }
}
