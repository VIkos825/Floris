namespace Floris.Settings
{
    /// <summary>Сведения о курсовом проекте для окна «О проекте». Значения задаются в appsettings.json.</summary>
    public class ProjectInfoSettings
    {
        /// <summary>Имя раздела в файле конфигурации.</summary>
        public const string SectionName = "ProjectInfo";

        /// <summary>Тема курсового проекта.</summary>
        public string Topic { get; set; } = string.Empty;

        /// <summary>Фамилия, имя, отчество исполнителя.</summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>Руководитель проекта.</summary>
        public string Supervisor { get; set; } = string.Empty;

        /// <summary>Учебное заведение.</summary>
        public string College { get; set; } = string.Empty;

        /// <summary>Год выполнения.</summary>
        public string Year { get; set; } = string.Empty;

        /// <summary>Версия приложения (см. журнал версий в записке).</summary>
        public string Version { get; set; } = string.Empty;
    }
}
