using microscore.application.interfaces.repositories;
using microscore.domain.entities.People;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;
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
            try
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
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error cuando se consulta los clientes.");
                throw;
            }
        }

        public override async Task<Client> GetByIdAsync(Guid id)
        {

            try
            {
                return await _dbContext.Client
                   .Include(c => c.PersonNav)
                   .Include(c => c.AccountsNav)
                   .AsNoTracking()
                   .FirstAsync(c => c.ClientId == id);
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error cuando se consulta el cliente por Id.");
                throw;
            }
        }

        public async Task<Client> GetClientByName(string name)
        {
            try
            {
                return await _dbContext.Client
                .Include(c => c.PersonNav)
                .Include(c => c.AccountsNav)
                .AsNoTracking()
                .Where(c => c.PersonNav.Name.Contains(name)).FirstAsync();
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error cuando se consulta el cliente por nombre.");
                throw;
            }
        }

    }
}
