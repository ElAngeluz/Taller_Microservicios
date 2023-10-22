using microscore.adapters.context;
using microscore.application.interfaces.repositories;
using microscore.domain.entities.People;
using microscore.infrastructure.abstracInfra;
using Microsoft.Extensions.Logging;

namespace microscore.infrastructure.data.repositories
{
    public class PersonRepository : GenericRepositoryAsync<Person>, IPersonRepository
    {
        public PersonRepository(MicrosContext dbContext, ILogger<PersonRepository> Logger) : base(dbContext, Logger)
        {
        }
    }
}
