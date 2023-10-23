using microscore.application.models.dtos.people;

namespace microscore.application.interfaces.services
{
    public interface IClientsServices
    {
        Task<ClientDTO> CreateClient(ClientDTO client);
        Task UpdateClient(ClientDTO client);
        Task<bool> DeleteClient(Guid Id);
        Task<List<ClientDTO>> GetAll(bool status);
        Task<ClientDTO> GetClient(Guid Id);
    }
}
