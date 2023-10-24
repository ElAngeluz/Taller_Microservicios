using microscore.application.interfaces.abstractapp;
using microscore.domain.entities.People;

namespace microscore.application.interfaces.repositories
{
    public interface IClientRepository : IGenericRepositoryAsync<Client>
    {
        Task<Client> GetClientByName(string name);
    }
}
