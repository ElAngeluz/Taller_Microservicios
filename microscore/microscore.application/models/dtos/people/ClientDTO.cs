using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.people
{
    public class ClientDTO
    {
        [JsonPropertyName("clienteId")]
        public Guid ClientId { get; set; }
        [JsonIgnore]
        public Guid PersonId { get; set; }
        [JsonPropertyName("nombre")]
        public string Name { get; set; }
        [JsonPropertyName("identificacion")]
        public string Identification { get; set; }
        [JsonPropertyName("direccion")]
        public string Address { get; set; }
        [JsonPropertyName("telefono")]
        public string Phone { get; set; }
        [JsonPropertyName("estado")]
        public bool State { get; set; }
        [JsonPropertyName("contraseña")]
        public string Password { get; set; }

    }
}
