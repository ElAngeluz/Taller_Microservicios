using microscore.application.interfaces.repositories;
using microscore.domain.entities.Accounts;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;

namespace microscore.infrastructure.data.repositories
{
    public class MovementRepository : GenericRepositoryAsync<Movement>, IMovementRepository
    {
        public MovementRepository(MicrosContext dbContext) : base(dbContext)
        {
        }
    }
}
