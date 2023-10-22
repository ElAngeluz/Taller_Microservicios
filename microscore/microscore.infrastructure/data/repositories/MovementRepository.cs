using microscore.adapters.context;
using microscore.application.interfaces.repositories;
using microscore.domain.entities.Accounts;
using microscore.infrastructure.abstracInfra;
using Microsoft.Extensions.Logging;

namespace microscore.infrastructure.data.repositories
{
    public class MovementRepository : GenericRepositoryAsync<Movement>, IMovementRepository
    {
        public MovementRepository(MicrosContext dbContext, ILogger<MovementRepository> Logger) : base(dbContext, Logger)
        {
        }
    }
}
