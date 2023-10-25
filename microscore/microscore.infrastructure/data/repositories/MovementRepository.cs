using microscore.application.interfaces.repositories;
using microscore.domain.entities.Accounts;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace microscore.infrastructure.data.repositories
{
    public class MovementRepository : GenericRepositoryAsync<Movement>, IMovementRepository
    {
        private readonly MicrosContext microsContext;
        public MovementRepository(MicrosContext dbContext) : base(dbContext)
        {
            microsContext = dbContext;
        }

        public override async Task<Movement> AddAsync(Movement entity)
        {

            try
            {
                Log.Information("Se procede con el ingreso de la entidad de movimiento.");
                entity.State = true;
                entity.Date = DateTime.Now;
                await microsContext.Movement.AddAsync(entity);
                await microsContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error al obtener todos los registros de la entidad.");
                throw;
            }
        }

        public async Task<IEnumerable<Movement>> GetAllAsync(DateTime DateTrx, string ClientName) => await microsContext.Movement
                .Include(a => a.AccountNav)
                    .ThenInclude(c => c.ClientNav)
                    .ThenInclude(p => p.PersonNav)
                .Where(m => EF.Functions.DateDiffDay( m.Date, DateTrx)==0 && m.AccountNav.ClientNav.PersonNav.Name == ClientName)
                .ToListAsync();
    }
}
