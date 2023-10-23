using Asp.Versioning;
using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.application.models.dtos.people;
using microscore.domain.entities.People;
using microscore.infrastructure.data.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace microscore.api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("/cliente")]
    public class ClientsController : BaseApiController
    {
        private readonly IClientsServices _IClientsServices;

        public ClientsController(IClientsServices iClientsServices)
        {
            _IClientsServices = iClientsServices;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<MsDtoResponse<List<ClientDTO>>>> GetAllClient(bool activos)
        {
            Log.Information("Comienza el proceso de consulta de clientes");
            var client = await _IClientsServices.GetAll(activos);
            return Ok(new MsDtoResponse<List<ClientDTO>>(client, HttpContext.TraceIdentifier));
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsDtoResponse<ClientDTO>>> GetClient(Guid id)
        {
            
            var client = await _IClientsServices.GetClient(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(new MsDtoResponse<ClientDTO>(client, HttpContext.TraceIdentifier)); 
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutClient(ClientDTO client)
        {
            await _IClientsServices.UpdateClient(client);
            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientDTO client)
        {
            if (string.IsNullOrEmpty(client.Name))
            {
                throw new ArgumentNullException(nameof(client.Name), "El cliente debe tener un nombre");
            }

            client = await _IClientsServices.CreateClient(client);

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {

            var client = await _IClientsServices.GetClient(id);

            if (client == null)
            {
                return NotFound();
            }
            
            _ = await _IClientsServices.DeleteClient(id);

            return NoContent();
        }

    }
}
