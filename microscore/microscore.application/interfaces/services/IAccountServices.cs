using microscore.application.models.dtos.accounts;

namespace microscore.application.interfaces.services
{
    public interface IAccountServices
    {
        Task<AccountDTO> GetAccountByNumber(string Number);
        Task<List<AccountDTO>> GetAccountByClient(string Identification, bool OnlyActive);
        Task<AccountDTO> CreateAccount(AccountRequest account);
        Task<AccountDTO> UpdateAccount(AccountRequest account);
        Task<AccountDTO> DeleteAccount(Guid Id);
        Task<AccountDTO> GetAccountByID(Guid Id);
    }
}
