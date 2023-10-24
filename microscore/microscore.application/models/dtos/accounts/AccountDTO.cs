using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.accounts
{
    public class AccountDTO : AccountRequest
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

    }
}
