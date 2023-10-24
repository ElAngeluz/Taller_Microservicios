using microscore.application.interfaces.repositories;
using microscore.domain.entities.Accounts;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace microscore.infrastructure.data.repositories
{
    public class AccountRepository : GenericRepositoryAsync<Account>, IAccountRepository
    {
        public readonly MicrosContext _dbContext;
        public AccountRepository(MicrosContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Account>> GetAllAsync(bool state = true)
        {
            try
            {
                return await _dbContext.Account
                        .Include(c => c.ClientNav)
                            .ThenInclude(t => t.PersonNav)
                        .AsNoTracking()
                        .ToListAsync();
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "se produjo un error al consultar todas las cuentas.");
                throw;
            }
        }

        public async Task<Account> GetAccountbyNumberAsync(string number)
        {
            try
            {
                return await _dbContext.Account
                        .Include(c => c.ClientNav)
                            .ThenInclude(t => t.PersonNav)
                        .AsNoTracking()
                        .Where(c => c.Number == number)
                        .SingleAsync();
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "se produjo un error al consultar la cuenta por el numero.");
                throw;
            }
        }


    }
}
