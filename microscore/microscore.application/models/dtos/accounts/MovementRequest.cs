using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.accounts
{
    public class MovementRequest
    {
        [JsonPropertyName("cuenta")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("tipo")]
        public string AccountTypeString { get; set; }

        [JsonPropertyName("movimiento")]
        public string Movement { get; set; }


    }
}
