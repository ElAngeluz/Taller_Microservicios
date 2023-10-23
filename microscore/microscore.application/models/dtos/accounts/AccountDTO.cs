using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace microscore.application.models.dtos.accounts
{
    public class AccountDTO
    {
        [JsonPropertyName("Cuenta")]
        public string Number { get; set; }
        [JsonPropertyName("tipo")]
        public string TypeAccount{ get; set; }
        [JsonPropertyName("saldo")]
        public decimal Balance { get; set; }
        [JsonPropertyName("estado")]
        public bool State { get; set; }
        [JsonPropertyName("cliente")]
        public string ClientName { get; set; }
    }
}
