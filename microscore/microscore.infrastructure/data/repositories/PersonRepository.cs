using microscore.application.interfaces.repositories;
using microscore.domain.entities.People;
using microscore.infrastructure.abstractInfra;
using microscore.infrastructure.data.context;

namespace microscore.infrastructure.data.repositories
{
    public class PersonRepository : GenericRepositoryAsync<Person>, IPersonRepository
    {
        public PersonRepository(MicrosContext dbContext) : base(dbContext)
        {
        }
    }
}
