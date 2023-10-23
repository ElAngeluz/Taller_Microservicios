namespace microscore.application.models.dtos
{
    public class MsDtoError
    {
        /// <summary>
        /// Código http.
        /// </summary>
        /// <example>400</example>
        public int Code { get; set; }

        /// <summary>
        /// Mensaje de error.
        /// </summary>
        /// <example>Error Aplicativo</example>
        public string Message { get; set; }
    }
}
