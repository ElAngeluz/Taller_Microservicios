using microscore.application.models.dtos.accounts;

namespace microscore.application.interfaces.services
{
    public interface IAccountServices
    {
        Task<AccountDTO> GetAccountByNumberAsync(string Number);
        Task<List<AccountDTO>> GetAccountByClient(string Identification, bool OnlyActive);
        Task<AccountDTO> CreateAccount(AccountRequest account);
        Task UpdateAccountAsync(AccountDTO account);
        Task<bool> DeleteAccount(Guid Id);
        Task<AccountDTO> GetAccountByID(Guid Id);
    }
}
