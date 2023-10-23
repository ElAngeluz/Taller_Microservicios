using microscore.application.models.dtos.accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microscore.application.interfaces.services
{
    public interface IAccountServices
    {
        Task<AccountDTO> GetAccountByNumber(string Number);
        Task<List<AccountDTO>> GetAccountByClient(string Identification, bool OnlyActive);
        Task<AccountDTO> CreateAccount(AccountDTO account);
        Task<AccountDTO> UpdateAccount(AccountDTO account);
        Task<AccountDTO> DeleteAccount(Guid Id);
    }
}
