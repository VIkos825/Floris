using Floris.Data;
using Floris.Models;
using Microsoft.EntityFrameworkCore;

namespace Floris.Services
{
    /// <summary>Реализация <see cref="IStoreService"/> на Entity Framework Core.</summary>
    public class StoreService : IStoreService
    {
        private readonly FlorisDbContext _db;

        /// <summary>Создаёт сервис.</summary>
        /// <param name="db">Контекст базы данных.</param>
        public StoreService(FlorisDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<City>> GetCitiesWithStoresAsync(CancellationToken ct = default)
        {
            var cities = await _db.Cities.AsNoTracking()
                .Include(c => c.Stores.Where(s => s.IsActive).OrderBy(s => s.Name))
                .OrderBy(c => c.Name)
                .ToListAsync(ct);

            return cities.Where(c => c.Stores.Count > 0).ToList();
        }

        /// <inheritdoc />
        public async Task<Store?> GetActiveStoreAsync(int storeId, CancellationToken ct = default)
        {
            return await _db.Stores.AsNoTracking()
                .Include(s => s.City)
                .FirstOrDefaultAsync(s => s.Id == storeId && s.IsActive, ct);
        }
    }
}
