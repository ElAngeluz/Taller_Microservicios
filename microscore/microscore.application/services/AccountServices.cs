using AutoMapper;
using microscore.application.interfaces.repositories;
using microscore.application.interfaces.services;
using microscore.application.models.dtos.accounts;
using microscore.domain.entities.Accounts;
using Serilog;

namespace microscore.application.services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _IAccountRepository;
        private readonly IClientRepository _ClientRepository;
        private readonly IMapper _mapper;

        public AccountServices(IAccountRepository iAccountRepository,
                               IMapper mapper,
                               IClientRepository clientRepository)
        {
            _IAccountRepository = iAccountRepository;
            _mapper = mapper;
            _ClientRepository = clientRepository;
        }

        public async Task<AccountDTO> CreateAccount(AccountRequest accountReq)
        {
            try
            {
                Account account = new();
                account = _mapper.Map<Account>(accountReq);
                var result = await _ClientRepository.GetClientByName(accountReq.ClientName);
                account.ClientId = result.ClientId;
                AccountDTO accountDTO = (AccountDTO)accountReq;
                accountDTO.Id = (await _IAccountRepository.AddAsync(account)).ClientId;

                return accountDTO;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error cuando se crea la cuenta.");
                throw;
            }

        }

        public async Task<AccountDTO> DeleteAccount(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountDTO>> GetAccountByClient(string Identification, bool OnlyActive)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDTO> GetAccountByID(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDTO> GetAccountByNumber(string Number)
        {
            try
            {
                return _mapper.Map<AccountDTO>(await _IAccountRepository.GetAccountbyNumberAsync(Number));
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error cuando se consultaba la cuenta por numero");
                throw;
            }
        }

        public async Task<AccountDTO> UpdateAccount(AccountRequest accountDTO)
        {
            throw new NotImplementedException();
        }
    }
}
