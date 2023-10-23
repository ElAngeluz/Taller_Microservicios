namespace microscore.application.models.dtos
{
    public class MsDtoResponse<T>
    {
        /// <summary>
        /// Identificador de trazabilidad.
        /// </summary>
        /// <example>6ee1b7a7bcd2c7c9</example>
        public string Traceid { get; set; }

        /// <summary>
        /// Datos devueltos.
        /// </summary>
        /// <example>{datos}</example>
        public T Data { get; set; }

        public MsDtoResponse(T data, string traceId)
        {
            Traceid = traceId;
            Data = data;
        }
    }
}
