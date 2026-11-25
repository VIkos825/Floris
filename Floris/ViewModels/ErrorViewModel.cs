namespace Floris.ViewModels
{
    /// <summary>Данные для страницы ошибки.</summary>
    public class ErrorViewModel
    {
        /// <summary>HTTP-код ошибки.</summary>
        public int StatusCode { get; set; } = 500;

        /// <summary>Понятное пользователю сообщение.</summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>Идентификатор запроса (для поиска в журнале).</summary>
        public string? RequestId { get; set; }
    }
}
