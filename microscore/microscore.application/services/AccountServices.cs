using microscore.application.interfaces.repositories;
using microscore.application.interfaces.services;
using microscore.application.models.dtos.accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microscore.application.services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _IAccountRepository;

        public AccountServices(IAccountRepository iAccountRepository)
        {
            _IAccountRepository = iAccountRepository;
        }

        public async Task<AccountDTO> CreateAccount(AccountDTO account)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDTO> DeleteAccount(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountDTO>> GetAccountByClient(string Identification, bool OnlyActive)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDTO> GetAccountByNumber(string Number)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDTO> UpdateAccount(AccountDTO account)
        {
            throw new NotImplementedException();
        }
    }
}
