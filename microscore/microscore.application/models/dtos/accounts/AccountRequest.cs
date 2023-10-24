using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.accounts
{
    public class AccountRequest
    {
        [Required]
        [JsonPropertyName("Cuenta")]
        public string Number { get; set; }

        [Required]
        [JsonPropertyName("tipo")]
        public string TypeAccount { get; set; }

        [JsonPropertyName("saldo")]
        public decimal Balance { get; set; } = 0;

        [JsonPropertyName("estado")]
        public bool State { get; set; } = true;

        [Required]
        [JsonPropertyName("cliente")]
        public string ClientName { get; set; }
    }
}
