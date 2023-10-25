using microscore.domain.Enums;
using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.accounts
{
    public class MovementReportDTO
    {
        [JsonPropertyName("Fecha")]
        public DateTime Date { get; set; }

        [JsonPropertyName("Cliente")]
        public string Name { get; set; }

        [JsonPropertyName("Numero Cuenta")]
        public string Number { get; set; }

        [JsonPropertyName("Tipo")]
        public string AccountType { get; set; }

        [JsonPropertyName("Saldo Inicial")]
        public decimal ValueBalance { get; set; }

        [JsonPropertyName("estado")]
        public bool State { get; set; }

        [JsonPropertyName("Movimiento")]
        public decimal Value { get; set; }

        [JsonPropertyName("Saldo Disponible")]
        public decimal Balance { get; set; }
    }
}
