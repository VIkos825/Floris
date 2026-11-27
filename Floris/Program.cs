using Floris.Data;
using Floris.Services;
using Floris.Settings;
using Microsoft.EntityFrameworkCore;

namespace Floris
{
    /// <summary>Точка входа приложения Floris.</summary>
    public class Program
    {
        /// <summary>Настраивает сервисы, применяет миграции, заполняет БД и запускает веб-сервер.</summary>
        /// <param name="args">Аргументы командной строки.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<FlorisDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<ProjectInfoSettings>(
                builder.Configuration.GetSection(ProjectInfoSettings.SectionName));

            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IStoreSelectionService, StoreSelectionService>();
            builder.Services.AddScoped<ICatalogService, CatalogService>();

            var app = builder.Build();

            InitializeDatabase(app);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        /// <summary>Применяет миграции и заполняет пустую базу начальными данными.</summary>
        /// <param name="app">Собранное приложение.</param>
        private static void InitializeDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<FlorisDbContext>();
                db.Database.Migrate();
                DbSeeder.Seed(db);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Не удалось подготовить базу данных. Проверьте строку подключения и миграции.");
                throw;
            }
        }
    }
}
