using System.Globalization;

namespace Floris.Helpers
{
    /// <summary>Расширения для вывода денежных сумм.</summary>
    public static class MoneyExtensions
    {
        private static readonly CultureInfo Russian = CultureInfo.GetCultureInfo("ru-RU");

        /// <summary>Форматирует сумму как «3 500 ₽».</summary>
        /// <param name="amount">Сумма в рублях.</param>
        /// <returns>Строка с разделителями тысяч и знаком рубля.</returns>
        public static string ToRub(this decimal amount)
        {
            return amount.ToString("N0", Russian) + " ₽";
        }
    }
}
