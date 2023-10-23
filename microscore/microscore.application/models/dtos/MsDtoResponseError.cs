namespace microscore.application.models.dtos
{
    public class MsDtoResponseError
    {
        /// <summary>
        /// Código http.
        /// </summary>
        /// <example>400</example>
        public int Code { get; set; }

        /// <summary>
        /// Identificador de trazabilidad.
        /// </summary>
        /// <example>6ee1b7a7bcd2c7c9</example>
        public string Traceid { get; set; }

        /// <summary>
        /// Mensaje de error.
        /// </summary>
        /// <example>Error Aplicativo</example>
        public string Message { get; set; }

        public List<MsDtoError> Errors { get; set; }

    }
}
