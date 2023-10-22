using microscore.adapters.context;
using microscore.application.interfaces.repositories;
using microscore.domain.entities.People;
using microscore.infrastructure.abstracInfra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace microscore.infrastructure.data.repositories
{
    public class ClientRepository : GenericRepositoryAsync<Client>, IClientRepository
    {
        private readonly ILogger _logger;
        private readonly MicrosContext _dbContext;
        public ClientRepository(MicrosContext dbContext, ILogger<ClientRepository> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbContext.Client
                .Include(c => c.PersonNav)
                .Include(c => c.AccountsNav)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Client> GetByIdAsync(Guid id)
        {
            return await _dbContext.Client
                .Include(c => c.PersonNav)
                .Include(c => c.AccountsNav)
                .FirstAsync(c => c.ClientId == id);
        }

        public async Task<Client> GetByIdAsync(string id)
        {
            return await _dbContext.Client
                .Include(c => c.PersonNav)
                .Include(c => c.AccountsNav)
                .FirstAsync(c => c.PersonNav.Identification == id);
        }
    }
}
