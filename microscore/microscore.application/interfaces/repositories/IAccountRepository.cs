using microscore.application.interfaces.abstractapp;
using microscore.domain.entities.Accounts;

namespace microscore.application.interfaces.repositories
{
    public interface IAccountRepository : IGenericRepositoryAsync<Account>
    {
        Task<Account> GetAccountbyNumberAsync(string number);
    }
}
