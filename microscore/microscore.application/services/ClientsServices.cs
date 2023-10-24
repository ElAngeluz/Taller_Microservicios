using AutoMapper;
using microscore.application.interfaces.repositories;
using microscore.application.interfaces.services;
using microscore.application.models.dtos.people;
using microscore.domain.entities.People;
using Serilog;

namespace microscore.application.services
{
    public class ClientsServices : IClientsServices
    {
        private readonly IClientRepository _IClientRepository;
        private readonly IPersonRepository _IPersonRepository;
        private readonly IMapper _mapper;

        public ClientsServices(IClientRepository iClientRepository,
                               IPersonRepository iPersonRepository,
                               IMapper mapper)
        {
            _IClientRepository = iClientRepository;
            _IPersonRepository = iPersonRepository;
            _mapper = mapper;
        }

        public async Task<ClientDTO> CreateClient(ClientDTO client)
        {
            try
            {
                Client data = new() { PersonNav = new Person() };
                data = _mapper.Map<Client>(client);
                client.ClientId = data.ClientId = Guid.NewGuid();
                data.PersonNav.Id = Guid.NewGuid();
                data.PersonNav.State = true;

                _ = await _IClientRepository.AddAsync(data);
                return client;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error al intentar ingresar el cliente.");
                throw;
            }
        }

        public async Task<List<ClientDTO>> GetAll(bool state)
        {
            try
            {
                var client = await _IClientRepository.GetAllAsync(state);
                return _mapper.Map<List<ClientDTO>>(client.ToList());
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, $"Error al consultar todos los clientes con estado: {state}");
                throw;
            }
        }

        public async Task<ClientDTO> GetClient(Guid Id)
        {
            try
            {
                var client = await _IClientRepository.GetByIdAsync(Id);
                return _mapper.Map<ClientDTO>(client);
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, $"Error al consultar el cliente ID´: {Id}");
                throw;
            }
        }

        public async Task UpdateClient(ClientDTO clientDTO)
        {
            try
            {
                Client client = await _IClientRepository.GetByIdAsync(clientDTO.ClientId);
                client.PersonNav.Address = clientDTO.Address;
                client.Password = clientDTO.Password;
                client.PersonNav.Name = clientDTO.Name;
                client.PersonNav.Identification = clientDTO.Identification;
                client.PersonNav.Phone = clientDTO.Phone;
                client.State = clientDTO.State;
                await _IClientRepository.UpdateAsync(client);

            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, $"Se produjo un error en la actualziacion del cliente: {clientDTO.ClientId}");
                throw;
            }
        }

        public async Task<bool> DeleteClient(Guid Id)
        {
            try
            {
                Client client = await _IClientRepository.GetByIdAsync(Id);
                await _IClientRepository.DeleteAsync(client);
                return true;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, $"Se produjo un error en la eliminacion del cliente: {Id}");
                throw;
            }
        }
    }
}
