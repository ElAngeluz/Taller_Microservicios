using microscore.application.interfaces.abstractapp;
using microscore.domain.entities.People;

namespace microscore.application.interfaces.repositories
{
    public interface IPersonRepository : IGenericRepositoryAsync<Person>
    {
    }
}
