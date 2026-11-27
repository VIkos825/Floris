using System.Globalization;
using Floris.Models;

namespace Floris.Services
{
    /// <summary>Реализация <see cref="IStoreSelectionService"/>: выбор хранится в cookie, результат кэшируется на время запроса.</summary>
    public class StoreSelectionService : IStoreSelectionService
    {
        /// <summary>Имя cookie с идентификатором магазина.</summary>
        public const string CookieName = "floris_store_id";

        private const string CacheKey = "Floris.SelectedStore";

        private readonly IHttpContextAccessor _accessor;
        private readonly IStoreService _stores;

        /// <summary>Создаёт сервис.</summary>
        /// <param name="accessor">Доступ к текущему HTTP-запросу.</param>
        /// <param name="stores">Сервис магазинов.</param>
        public StoreSelectionService(IHttpContextAccessor accessor, IStoreService stores)
        {
            _accessor = accessor;
            _stores = stores;
        }

        /// <inheritdoc />
        public int? GetSelectedStoreId()
        {
            var context = _accessor.HttpContext;
            if (context != null
                && context.Request.Cookies.TryGetValue(CookieName, out var raw)
                && int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id)
                && id > 0)
            {
                return id;
            }
            return null;
        }

        /// <inheritdoc />
        public async Task<Store?> GetSelectedStoreAsync(CancellationToken ct = default)
        {
            var context = _accessor.HttpContext;
            if (context == null) return null;

            if (context.Items.TryGetValue(CacheKey, out var cached))
            {
                return cached as Store;
            }

            Store? store = null;
            var id = GetSelectedStoreId();
            if (id.HasValue)
            {
                store = await _stores.GetActiveStoreAsync(id.Value, ct);
                if (store == null)
                {
                    ClearSelection();
                }
            }

            context.Items[CacheKey] = store;
            return store;
        }

        /// <inheritdoc />
        public void SelectStore(int storeId)
        {
            var context = _accessor.HttpContext;
            if (context == null) return;

            context.Response.Cookies.Append(
                CookieName,
                storeId.ToString(CultureInfo.InvariantCulture),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    HttpOnly = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                });
            context.Items.Remove(CacheKey);
        }

        /// <inheritdoc />
        public void ClearSelection()
        {
            var context = _accessor.HttpContext;
            if (context == null) return;

            context.Response.Cookies.Delete(CookieName);
            context.Items.Remove(CacheKey);
        }
    }
}
