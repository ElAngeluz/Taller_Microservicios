using microscore.application.interfaces.repositories;
using microscore.domain.entities.Accounts;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace microscore.infrastructure.data.repositories
{
    public class AccountRepository : GenericRepositoryAsync<Account>, IAccountRepository
    {
        public readonly MicrosContext _dbContext;
        public AccountRepository(MicrosContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Account>> GetAllAsync(bool status = true) =>
            await _dbContext.Account
                        .Include(c => c.ClientNav)
                            .ThenInclude(t => t.PersonNav)
                        .AsNoTracking()
                        .ToListAsync();

    }
}
