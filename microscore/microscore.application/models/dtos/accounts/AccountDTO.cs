using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.accounts
{
    public class AccountDTO : AccountRequest
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

        public AccountDTO(AccountRequest accountRequest)
        {
            Number = accountRequest.Number;
            TypeAccount = accountRequest.TypeAccount;
            Balance = accountRequest.Balance;
            State = accountRequest.State;
            ClientName = accountRequest.ClientName;
        }
    }
}
