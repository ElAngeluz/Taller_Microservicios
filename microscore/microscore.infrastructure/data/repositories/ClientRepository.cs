using microscore.application.interfaces.repositories;
using microscore.domain.entities.People;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace microscore.infrastructure.data.repositories
{
    public class ClientRepository : GenericRepositoryAsync<Client>, IClientRepository
    {
        private readonly MicrosContext _dbContext;
        public ClientRepository(MicrosContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Client>> GetAllAsync(bool status = true)
        {
            
            return status ? await _dbContext.Client
                .Include(c => c.PersonNav)
                .Include(c => c.AccountsNav)
                .Where(c => c.State == status)
                .AsNoTracking()
                .ToListAsync() :
                await _dbContext.Client
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

    }
}
