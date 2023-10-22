using microscore.adapters.context;
using microscore.application.interfaces.repositories;
using microscore.domain.entities.Accounts;
using microscore.infrastructure.abstracInfra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace microscore.infrastructure.data.repositories
{
    public class AccountRepository : GenericRepositoryAsync<Account>, IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;
        public readonly MicrosContext _dbContext;
        public AccountRepository(MicrosContext dbContext, ILogger<AccountRepository> Logger) : base(dbContext, Logger)
        {
            _logger = Logger;
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Account>> GetAllAsync() =>
            await _dbContext.Account
                        .Include(c => c.ClientNav)
                            .ThenInclude(t => t.PersonNav)
                        .AsNoTracking()
                        .ToListAsync();

    }
}
